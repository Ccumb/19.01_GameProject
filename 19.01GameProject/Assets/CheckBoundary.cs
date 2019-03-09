using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.AI;
using Neremnem.Tools;
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CheckBoundary : MonoBehaviour
{
    private SphereCollider mSphereCollider;
    private Rigidbody mRigidbody;
    private int mCountInBoundary = 0;
    private float mTime;
    private void Awake()
    {
        mSphereCollider = GetComponent<SphereCollider>();
        mSphereCollider.radius = 3.0f;
        mSphereCollider.isTrigger = true;

        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.isKinematic = true;
        mRigidbody.useGravity = false;
        BlackBoard.AddKey("Whirlwind", false);
    }
    private void Start()
    {
        mSphereCollider.enabled = false;
    }
    private void OnEnable()
    {
        EventManager.StartListeningCommonEvent("ActiveBoundary", EnabledCollider);
        EventManager.StartListeningCommonEvent("InactiveBoundary", DisabledCollider);
    }
    private void OnDisable()
    {
        EventManager.StopListeningCommonEvent("ActiveBoundary", EnabledCollider);
        EventManager.StopListeningCommonEvent("InactiveBoundary", DisabledCollider);
    }
    private void EnabledCollider()
    {
        Debug.Log("활성화!");
        mSphereCollider.enabled = true;
    }
    private void DisabledCollider()
    {
        mSphereCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {        
        if(other.tag.Equals("Player"))
        {
            Debug.Log("호출");
            mCountInBoundary++;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (mCountInBoundary > 0)
            {
                mTime += Time.deltaTime;
            }
            if (mTime >= 0.5f)
            {
                BlackBoard.SetValue("Whirlwind", true);
                Debug.Log("tre");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            mTime = 0f;
            mCountInBoundary--;
            BlackBoard.SetValue("Whirlwind", false);
        }
    }
}
