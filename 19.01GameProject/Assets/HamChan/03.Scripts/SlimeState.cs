using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private void Start()
        {
            BlackBoard.SetValueByStringKey("Phase1", "Phase1");
            mHP = 100;
        }
        private void Update()
        {
            if (mHP < 21)
            {
                BlackBoard.DeleteStringKey("Phase4");
                BlackBoard.SetValueByStringKey("Phase5", "Phase5");
            }
            else if (mHP < 41)
            {
                BlackBoard.DeleteStringKey("Phase3");
                BlackBoard.SetValueByStringKey("Phase4", "Phase4");
            }
            else if (mHP < 61)
            {
                BlackBoard.DeleteStringKey("Phase2");
                BlackBoard.SetValueByStringKey("Phase3", "Phase3");
            }
            else if (mHP < 81)
            {
                BlackBoard.DeleteStringKey("Phase1");
                BlackBoard.SetValueByStringKey("Phase2", "Phase2");
            }

        }
    }
}
