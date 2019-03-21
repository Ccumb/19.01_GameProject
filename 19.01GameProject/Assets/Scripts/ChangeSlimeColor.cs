using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class ChangeSlimeColor : MonoBehaviour
{
    public bool bIsAttack;
    Renderer modelRenderer;

    void Start()
    {
        modelRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    private void OnEnable()
    {
        EventManager.StartListeningCommonEvent("ChangeAttackColor", ChangeAttackColor);
        EventManager.StartListeningCommonEvent("ChangeNormalColor", ChangeNormalColor);
    }
    private void OnDisable()
    {
        EventManager.StartListeningCommonEvent("ChangeAttackColor", ChangeAttackColor);
        EventManager.StartListeningCommonEvent("ChangeNormalColor", ChangeNormalColor);
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
    private void ChangeAttackColor()
    {
        bIsAttack = true;
    }
    private void ChangeNormalColor()
    {
        bIsAttack = false;
    }
}
