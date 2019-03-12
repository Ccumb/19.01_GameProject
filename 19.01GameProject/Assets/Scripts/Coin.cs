using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour
{
    public int gold;
    public float maintainingTime;

    private void OnEnable()
    {
        StartCoroutine("Maintaining");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Playera player = collision.gameObject.GetComponent<Playera>();
            player.UpdateGold(gold);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Maintaining()
    {
        yield return new WaitForSeconds(maintainingTime);

        this.gameObject.SetActive(false);
    }
}
