using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveHorizontal = moveHorizontal * Time.deltaTime;
        moveVertical = moveVertical * Time.deltaTime;
        transform.Translate(Vector3.right * moveHorizontal * 6);
        transform.Translate(Vector3.forward * moveVertical * 6);
    }

}
