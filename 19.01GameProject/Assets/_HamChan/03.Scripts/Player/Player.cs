using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
public class Player : MonoBehaviour
{
    public InputManager linkedInputManager;
    public PlayerStatus playerStatus;
    public Animator animator;
    public string playerID = "Player1";

    protected virtual void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        GetInputManager();
    }
    protected virtual void GetInputManager()
    {
        linkedInputManager = null;
        InputManager[] foundInputManagers
            = FindObjectsOfType(typeof(InputManager))
            as InputManager[];
        foreach (InputManager foundInputManager in foundInputManagers)
        {
            if (foundInputManager.PlayerID == playerID)
            {
                linkedInputManager = foundInputManager;
            }
        }
    }
    public virtual void SetPaleyrID(string id)
    {
        playerID = id;
        GetInputManager();
    }
}
