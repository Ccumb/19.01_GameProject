using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
[RequireComponent(typeof(Player))]
public class PlayerAbility : MonoBehaviour
{
    protected Player mPlayer;
    protected InputManager mInputManager;
    protected PlayerStatus mPlayerStatus;
    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        mPlayer = GetComponent<Player>();
        mInputManager = mPlayer.linkedInputManager;
        mPlayerStatus = mPlayer.playerStatus;
    }
}
