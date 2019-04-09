using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class Coin : MonoBehaviour
{
    public float RepulsiveForce = 10.0f;
    public int Gold = 0; //골드 양
    Rigidbody CoinRigid = null;

    private void Awake()
    {
        CoinRigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CoinRigid.velocity = new Vector3(0, 1, 0) * RepulsiveForce;
    }

    private void OnDisable()
    {
        CoinRigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //골드 획득 코드 차후 추가
            Debug.Log("GetGold: " + Gold);
            gameObject.SetActive(false);
        }
    }

}
