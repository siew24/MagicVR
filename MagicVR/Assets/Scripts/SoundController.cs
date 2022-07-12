using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource BGM, Troll;

    public void PlayBGM()
    {
        BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void PlayTroll()
    {
        StopBGM();
        Troll.Play();
    }

    public void StopTroll()
    {
        Troll.Stop();
    }
}
