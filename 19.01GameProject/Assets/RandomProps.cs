using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProps : MonoBehaviour
{
    public GameObject[] Props;

    void Start()
    {
        for(int i = 0; i < Props.Length; i++)
        {
            Props[i].SetActive(false);
        }
        Props[Random.Range(0, 6)].SetActive(true);
    }
}
