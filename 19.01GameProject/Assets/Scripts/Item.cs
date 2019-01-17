using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float respawnTime;
    protected bool mIsActive;

    protected IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        Active();
    }

    protected void Active()
    {
        if(mIsActive == false)
        {
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
