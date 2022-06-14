using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableEntity : MonoBehaviour
{

    public void Pause()
    {
        foreach  (IPausable pausable in GetComponents<IPausable>())
        {
            pausable.Pause();
        }
    }

    public void Resume()
    {
        foreach (IPausable pausable in GetComponents<IPausable>())
        {
            pausable.Resume();
        }
    }
}
