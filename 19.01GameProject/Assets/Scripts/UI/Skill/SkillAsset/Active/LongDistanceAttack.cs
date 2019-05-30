using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[RequireComponent(typeof(Rigidbody))]
/// <summary>
/// 원거리 공격 스킬에서 날아가는 프리팹에 대한 스크립트
/// </summary>
/// 
public class LongDistanceAttack : MonoBehaviour
{
    [Range(30.0f, 50.0f)]
    public float speed = 0f;


    private Player player;
    private Vector3 direction;
    private Rigidbody rig;
    private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z + 1.0f);
        direction = player.transform.forward;
        rig = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position) > 60.0f)
        {
            Destroy(this.gameObject);
        }
        //rig.velocity = player.transform.forward * speed * Time.deltaTime;        
        rig.AddForce(direction * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            EventManager.TriggerTakeDamageEvent("PlayersAttack",
                other.gameObject, Damage);
            Destroy(this.gameObject);
        }
        //Debug.Log(other.tag.ToString());
    }    
}
