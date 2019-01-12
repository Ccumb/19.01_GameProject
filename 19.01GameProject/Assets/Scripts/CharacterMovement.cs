using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 캐릭터 움직임 관련 컴포넌트
///</summary>
[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class CharacterMovement : MonoBehaviour
{
    public float speed = 0.08f;
    private Rigidbody mRigid;

    // Start is called before the first frame update
    void Start()
    {
        mRigid = GetComponent<Rigidbody>();
        mRigid.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.Translate(this.transform.forward * speed);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(-this.transform.forward * speed);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(-this.transform.right * speed);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(this.transform.right * speed);
        }
    }
}
