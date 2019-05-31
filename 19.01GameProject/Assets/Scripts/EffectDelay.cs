using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

public class EffectDelay : MonoBehaviour
{
    public Transform effectPos;
    public float startTime = 0;
    public float delayTime = 0;
    public GameObject effect;
    private GameObject mEffectInstance;

    // Start is called before the first frame update
    void Start()
    {
        if(effectPos == null)
        {
            effectPos = this.transform;
        }
        mEffectInstance = Instantiate(effect);
        mEffectInstance.SetActive(false);

        EventManager.StartListeningCommonEvent("CreateBoomEffect", StartEffect);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartEffect()
    {
        StartCoroutine("EffectCreate");
    }

    IEnumerator EffectCreate()
    {
        yield return new WaitForSeconds(startTime);
        mEffectInstance.SetActive(true);

        yield return new WaitForSeconds(delayTime);
        mEffectInstance.SetActive(false);
    }
}
