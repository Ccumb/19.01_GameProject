using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public class CharacterAttack : MonoBehaviour
{
    private BoxCollider mAttackBound;

    public float center_z;

    public float length;    // z
    public float height;    // y 
    public float width;     // x

    private float mPower;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        mAttackBound = GetComponent<BoxCollider>();

        mAttackBound.center = new Vector3(0.0f, 0.0f, center_z);
        mAttackBound.size = new Vector3(width, height, length);

        mAttackBound.isTrigger = true;
        mAttackBound.enabled = false;

        mPower = gameObject.GetComponentInParent<Player>().power;
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            StartCoroutine("Attack");
        }

        if (Input.GetKey("up"))
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey("down"))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        if (Input.GetKey("left"))
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        if (Input.GetKey("right"))
        {
            transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
    }

    IEnumerator Attack()
    {
        mAttackBound.enabled = true;

        yield return new WaitForSeconds(1.0f);

        mAttackBound.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(mPower);
        }
    }

    void OnDrawGizmos()
    {
        if(!isPlaying)
        {
            Gizmos.color = Color.yellow;

            Vector3 center = this.transform.position;

            center.z += center_z;

            Vector3 size = new Vector3(width, height, length);

            Gizmos.DrawCube(center, size);
        }
    }
}
