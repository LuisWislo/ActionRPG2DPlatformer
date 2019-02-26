using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private ParticleSystem thisPS;

    // Start is called before the first frame update
    void Start()
    {
        thisPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!thisPS.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
