using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    private BoxCollider mCollider;
    public string destination;

    private void Start()
    {
        mCollider = GetComponent<BoxCollider>();
        mCollider.isTrigger = true;
    }
    private void OnEnable()
    {
        //StartCoroutine("KeepPortal");
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
    private IEnumerator KeepPortal()
    {        
        yield return new WaitForSeconds(15f);
        this.gameObject.SetActive(false);
    }
}
