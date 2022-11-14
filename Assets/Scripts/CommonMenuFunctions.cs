using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMenuFunctions : MonoBehaviour
{
   public void LoadScene(int index)
   {
        SceneLoader.Instance.LoadScene(index);
   }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
