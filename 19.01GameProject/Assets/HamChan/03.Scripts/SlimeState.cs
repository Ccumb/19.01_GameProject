﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;

namespace Neremnem.AI
{
    public enum ESlimePhase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }

    public class SlimeState : MonoBehaviour
    {
        private int mHP;
        private ESlimePhase mPhase;
        public ESlimePhase Phase
        {
            get { return mPhase; }
            set { mPhase = value; }
        }
        public int SlimeHP
        {
            get { return mHP; }
            set { mHP = value; }
        }
        private void Awake()
        {
            BlackBoard.AddStringKey("Phase", "Phase1");
            mHP = 100;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                mHP = 80;
                Debug.Log(mHP);
                BlackBoard.SetValueByStringKey("Phase","Phase2");
                EventManager.TriggerCommonEvent("ActiveBoundary");
            }
        }
        private void TakeDamage(int i)
        {
            mHP = mHP - i;
            if (mHP < 21)
            {
                BlackBoard.SetValueByStringKey("Phase", "Phase5");

            }
            else if (mHP < 41)
            {
                BlackBoard.SetValueByStringKey("Phase", "Phase4");

            }
            else if (mHP < 61)
            {
                BlackBoard.SetValueByStringKey("Phase", "Phase3");

            }
            else if (mHP < 81)
            {
                BlackBoard.SetValueByStringKey("Phase", "Phase2");
                EventManager.TriggerCommonEvent("ActiveBoundary");
            }
        }

    }
}
