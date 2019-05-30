using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "PassiveHeal", menuName = "Skills/Passive/PassiveHeal", order = 1)]
public class PassiveHeal : Skill, IUseable
{
    public float sec;
    private Player mPlayer;
    private bool isStop = false;
    private IEnumerator enumerator;

    public override void Use()
    {
        UIManager.MyInstance.StartRoutine(this.CorPassiveHeal());
    }
    //장착 해제를 안하고 인벤토리를 껏다가 켰을 때 한번 더 코루틴이 실행되서
    //체력이 두배로 오른다. 수정방법 생각하기
    public override void Relieve()
    {
        UIManager.MyInstance.StopRoutine(this.CorPassiveHeal());
        isStop = true;
    }
    public IEnumerator CorPassiveHeal()
    {
        while(true)
        {
            if(isStop)
            {
                isStop = false;
                yield break;
            }
            mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if(mPlayer.playerStatus.HP < mPlayer.playerStatus.MaxHP)
            {
                EventManager.TriggerIntEvent("EatHealthPosition", MyEffect);
            }
            yield return new WaitForSeconds(sec);
        }
    }
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.isPassive = true;
    }

}
