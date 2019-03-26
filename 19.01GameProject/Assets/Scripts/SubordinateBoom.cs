using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubordinateBoom : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //대미지 주는 함수
            Debug.Log("Damage!");
            gameObject.SetActive(false);
        }
    }
}
