using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : PlayerAbility
{
    protected bool mPause = false;
    private void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(mInputManager.PauseButton.State.CurrentState == Neremnem.Tools.NRMInput.EButtonStates.Down)
        {
            Debug.Log("pause");
            mPause = !mPause;
            if(mPause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
