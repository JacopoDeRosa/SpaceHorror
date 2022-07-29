using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAudio
{
    [SerializeField] private SurfaceTypes _surfaceType;
    [SerializeField] private AudioClip _audio;


    public override bool Equals(object obj)
    {
        SurfaceAudio audio = obj as SurfaceAudio;

        if(audio == null)
        {
            return false;
        }

        return true;
    }
/*
    public static bool operator ==(SurfaceAudio a, SurfaceAudio b)
    {
        return a.Equals(b);
    }
    public static bool operator !=(SurfaceAudio a, SurfaceAudio b)
    {
        return !(a == b);
    }
*/
}
