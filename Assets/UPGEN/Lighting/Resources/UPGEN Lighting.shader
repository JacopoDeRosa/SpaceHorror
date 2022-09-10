Shader "Hidden/Shader/UPGEN_Lighting"
{
	HLSLINCLUDE

	#define MAX_LIGHTS_COUNT 96

    #pragma target 4.5
    #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"

	#pragma multi_compile __ SSAO

	struct Attributes
    {
		uint vertexID : SV_VertexID;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

    struct Varyings
    {
		float4 positionCS : SV_POSITION;
		float2 texcoord   : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
		output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
		output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
		return output;
    }

	uniform float _Intensity;
	uniform float4x4 _LeftWorldFromView;
	uniform float4x4 _RightWorldFromView;
	uniform float4x4 _LeftViewFromScreen;
	uniform float4x4 _RightViewFromScreen;

	TEXTURE2D(_CameraColorTexture);
	SAMPLER(sampler_CameraColorTexture);

	TEXTURE2D_X_FLOAT(_SSAO_OcclusionTexture2);
	SAMPLER(sampler_SSAO_OcclusionTexture2);

	uniform int _LightsCount = 0;
	uniform float4 _LightsPositions[MAX_LIGHTS_COUNT]; // XYZ - position, W - range
	uniform float4 _LightsColors[MAX_LIGHTS_COUNT]; // RGB - color, A - not used

	float4 Frag(Varyings input) : SV_Target
	{
		float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord);
		float3 outColor = SAMPLE_TEXTURE2D_X(_CameraColorTexture, sampler_CameraColorTexture, uv).rgb;
		float depth = SampleSceneDepth(uv);
		float3 wpos = ComputeWorldSpacePosition(input.texcoord, depth, UNITY_MATRIX_I_VP);
		float3 normal = SampleSceneNormals(uv);

		// DEBUG
		//return float4(cos(wpos * 100), 1);
		//return float4(depth, depth, depth, 1);
		//return float4(normal, 1);
		//return float4(outColor, 1);

		int c = _LightsCount;
		float3 light; float3 dir; float dist; float4 lp;

		while (c >= 11) // loop unrolling
		{
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }

			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }

			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }

			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
		}

		while (c >= 3) // loop unrolling
		{
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
			c--; lp = _LightsPositions[c]; dir = lp.xyz - wpos; dist = length(dir); if (dist < lp.w) { float i = 1 - dist / lp.w; i *= i; i *= max(0, dot(normal.xyz, dir / dist)); light += _LightsColors[c].rgb * i; }
		}

		while (c > 0) // real iteration
		{
			c--;
			lp = _LightsPositions[c];
			dir = lp.xyz - wpos;
			dist = length(dir);
			if (dist < lp.w)
			{
				float i = 1 - dist / lp.w;
				i *= i;
				i *= max(0, dot(normal.xyz, dir / dist));
				light += _LightsColors[c].rgb * i;
			}
		}

		float3 fastLight = 0.05 * light * normalize(outColor.rgb + 0.001); // albedo approximation
#if SSAO
		fastLight *= SAMPLE_TEXTURE2D_X(_SSAO_OcclusionTexture2, sampler_SSAO_OcclusionTexture2, uv).rgb; // Add SSAO
#endif
		return float4(outColor.rgb + fastLight * _Intensity, 1);
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment Frag
                #pragma vertex Vert
            ENDHLSL
        }
    }

    Fallback Off
}