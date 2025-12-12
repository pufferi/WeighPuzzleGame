using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource src;
    public AudioClip c1,c2,c3;

    public void C1()
    {
        src.clip = c1;
        src.Play();
    }
    public void C2()
    {
        src.clip = c2;
        src.Play();
    }
    public void C3()
    {
        src.clip = c3;
        src.Play();
    }
}
