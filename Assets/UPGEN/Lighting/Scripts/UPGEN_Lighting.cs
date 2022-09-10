using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public sealed class UPGEN_Lighting : ScriptableRendererFeature
{
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private const string SHADER = "Hidden/Shader/UPGEN_Lighting";

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [Tooltip("Controls the intensity of all additional lighting")]
    [Range(0, 5)]
    public float intensity = 1;

    [Tooltip("Use output from SSAO to shade additional lighting")]
    public bool applySSAO;

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private UL_RenderPass _scriptablePass;

    public override void Create()
    {
        _scriptablePass = new UL_RenderPass { renderPassEvent = RenderPassEvent.AfterRenderingOpaques };
        _scriptablePass.ConfigureInput(ScriptableRenderPassInput.Color | ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Normal);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (intensity <= 0 || !UL_Renderer.HasLightsToRender) return;
        _scriptablePass.Setup(renderer, intensity, applySSAO, renderingData.cameraData.camera);
        renderer.EnqueuePass(_scriptablePass);
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public class UL_RenderPass : ScriptableRenderPass
    {
        private ScriptableRenderer _renderer;
        private RenderTargetHandle _temporaryColorTexture = new RenderTargetHandle();
        private Material _material;

        public UL_RenderPass() => _temporaryColorTexture.Init("_UPGENLightingRT");

        public void Setup(ScriptableRenderer renderer, float intensity, bool applySSAO, Camera camera)
        {
            _renderer = renderer;

            if (_material == null)
            {
                _material = new Material(Shader.Find(SHADER));
                if (_material == null) return;
            }
            if (applySSAO != _material.IsKeywordEnabled("SSAO"))
            {
                if (applySSAO) _material.EnableKeyword("SSAO");
                else _material.DisableKeyword("SSAO");
            }
            _material.SetFloat("_Intensity", intensity);
            UL_Renderer.SetupForCamera(camera, _material);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            switch (renderingData.cameraData.cameraType)
            {
                case CameraType.Game:
                case CameraType.SceneView: break;
                default: return;
            }

            var cmd = CommandBufferPool.Get("UPGEN_Lighting");
            var desc = renderingData.cameraData.cameraTargetDescriptor;
            desc.msaaSamples = 1;
            desc.depthBufferBits = 0;

            var source = _renderer.cameraColorTarget;
            cmd.GetTemporaryRT(_temporaryColorTexture.id, desc, FilterMode.Point);
            var destination = _temporaryColorTexture.Identifier();

            Blit(cmd, source, destination, _material);
            Blit(cmd, destination, source);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_temporaryColorTexture.id);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}