using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour {

    private void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }

    public void enableParticleSystem()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
