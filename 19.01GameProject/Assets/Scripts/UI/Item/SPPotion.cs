using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

[CreateAssetMenu(fileName = "SPPotion", menuName = "Items/Potion/SPPotion")]
public class SPPotion : ItemIconVersion, IUseable
{
    public int MyCost
    {
        get; set;
    }

    //회복량
    [SerializeField]
    private int mSPRec;

    public void Use()
    {
        // 플레이어 클래스 겹쳐서 일단 playera로 수정, 확인바람
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.playerStatus.SP < player.playerStatus.MaxSP)
        {
            Remove();
            EventManager.TriggerIntEvent("EatSPPotion", mSPRec);
        }
    }
}
