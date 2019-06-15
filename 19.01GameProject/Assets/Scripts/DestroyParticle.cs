using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private ParticleSystem mParticle;
    // Start is called before the first frame update
    void Start()
    {
        mParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mParticle.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}
