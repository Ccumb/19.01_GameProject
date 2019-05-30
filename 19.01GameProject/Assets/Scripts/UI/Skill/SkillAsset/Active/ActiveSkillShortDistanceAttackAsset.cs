using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
//플레이어 공격애니메이션 재생하면서
//주는 데미지만 업시켜줬다가 다시 다운시켜주는건 어떨까
[CreateAssetMenu(fileName = "SkillShortDistanceAttackAsset", menuName = "Skills/Active/SkillShortDistanceAttackAsset", order = 1)]
public class ActiveSkillShortDistanceAttackAsset : Skill, IUseable
{
    public override void Use()
    {
        if(this.MyIsUseable)
        {
            PlayerAttack player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
            EventManager.TriggerIntEvent("IncreasedAttackDamage", MyEffect);
            player.SetSkillAttackTrigger();
            UIManager.MyInstance.StartRoutine(this.ResetDamage());
        }
    }

    public IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1.5f);
        EventManager.TriggerIntEvent("IncreasedAttackDamage", -MyEffect);
    }
}
