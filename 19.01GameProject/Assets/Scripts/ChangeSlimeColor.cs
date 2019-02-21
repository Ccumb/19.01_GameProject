using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSlimeColor : MonoBehaviour
{
    public bool bIsAttack;
    Renderer modelRenderer;

    void Start()
    {
        modelRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsAttack == true)
        {
            modelRenderer.material.SetVector("_RimColor", new Vector4(1, 0, 0, 1));
        }
        else
        {
            modelRenderer.material.SetVector("_RimColor", new Vector4(1, 1, 1, 1));
        }
    }
}
