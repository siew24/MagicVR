using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockHandler : MonoBehaviour
{
    public ParticleSystem beam;
    public ParticleSystem chargeLine;

    // Start is called before the first frame update
    void Start()
    {
        beam.Play();
        chargeLine.Play();
    }
}
