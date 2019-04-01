using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    private BoxCollider mCollider;
    public string destination;
    void Start()
    {
        mCollider = GetComponent<BoxCollider>();
        mCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(destination);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(destination);
        }
    }
}
