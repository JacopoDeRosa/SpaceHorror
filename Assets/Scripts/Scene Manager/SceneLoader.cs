using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Graphic[] _loadingScreenImages;
    [SerializeField] private float _fadeTime;

    private Color _transparent, _normal;

    private void Awake()
    {
        if(FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
        _transparent = new Color(0, 0, 0, 0);
        _normal = Color.white;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneRoutine(index));
    }

    private IEnumerator LoadSceneRoutine(int index)
    {
        yield return ShowLoadingScreen();
        yield return SceneManager.LoadSceneAsync(index);
        yield return HideLoadingScreen();
    }

    private IEnumerator ShowLoadingScreen()
    {
        _loadingScreen.SetActive(true);
        yield return SmoothFadeBetween(_transparent, _normal);
    }

    private IEnumerator HideLoadingScreen()
    { 
        yield return SmoothFadeBetween(_normal, _transparent);
        _loadingScreen.SetActive(false);
    }

    private IEnumerator SmoothFadeBetween(Color a, Color b)
    {
        float t = 0;
        while (t < 1)
        {
            foreach (Graphic image in _loadingScreenImages)
            {
                image.color = Color.Lerp(a, b, t);
            }
            t += (1 / _fadeTime) * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
    }
}
