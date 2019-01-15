using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float respawnTime;
    protected bool mIsActive;

    protected IEnumerator Respawn()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(3.0f);
        Active();
    }

    protected void Active()
    {
        if(mIsActive == false)
        {
            Debug.Log("Respawn");
            MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            Collider collider = this.gameObject.GetComponent<Collider>();
            collider.enabled = true;

            mIsActive = true;
        }
    }

    protected void InActive()
    {
        if (mIsActive == true)
        {
            MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            Collider collider = this.gameObject.GetComponent<Collider>();
            collider.enabled = false;

            mIsActive = false;
        }
    }
}
