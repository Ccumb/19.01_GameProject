using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public KeyCode skill1 = KeyCode.Q;
    public KeyCode skill2 = KeyCode.W;
    public KeyCode skill3 = KeyCode.E;
    public KeyCode skill4 = KeyCode.R;

    [SerializeField]
    private int mMaxCost;

    [SerializeField]
    private Skill[] skills;

    private int mCurrentCost;

}
