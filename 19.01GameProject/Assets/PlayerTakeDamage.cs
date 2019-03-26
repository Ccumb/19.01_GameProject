using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class PlayerTakeDamage : PlayerAbility
{
    private void OnEnable()
    {
        EventManager.StartListeningTakeDamageEvent("EnemysAttack", TakeDamage);
    }
    private void OnDisable()
    {
        EventManager.StopListeningTakeDamageEvent("EnemysAttack", TakeDamage);
    }
    protected virtual void TakeDamage(GameObject gameObject, int damage)
    {
        if(gameObject == this.gameObject)
        {
            EventManager.TriggerIntEvent("TakeDamage", damage);
            Debug.Log(mPlayerStatus.HP);
        }
    }
}
