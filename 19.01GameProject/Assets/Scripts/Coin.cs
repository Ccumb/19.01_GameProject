using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class Coin : MonoBehaviour
{
    public float RepulsiveForce = 10.0f; //가해주는 힘
    public int Gold = 0; //골드 양
    Rigidbody CoinRigid = null; //RepulsiveForce가하기 위한 리지드바디

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
    /// <summary>
    /// 부딪혔을 때 처리, 인벤토리에 골드 추가 코인 액티브 false
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InventoryScript.MyInstance.UpdateGold(Gold);
            Debug.Log("GetGold: " + Gold);
            gameObject.SetActive(false);
        }
    }

}
