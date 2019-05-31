using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using NeRemNem.Tools;

public class ActiveCamera : MonoBehaviour
{
    private BoxCollider mBoxCollider;
    public GameObject secondCamera;

    // Start is called before the first frame update
    void Start()
    {
        mBoxCollider = GetComponent<BoxCollider>();
        secondCamera.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        secondCamera.active = true;
    }


    private void OnTriggerExit(Collider other)
    {
        secondCamera.active = false;
    }
}
