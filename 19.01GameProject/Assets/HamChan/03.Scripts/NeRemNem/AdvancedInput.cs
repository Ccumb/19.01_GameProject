using System.Collections;
using UnityEngine;

namespace Neremnem.Tools
{
    public class NRMInput : MonoBehaviour
    {
        public enum EButtonStates { Off, Down, Pressed, Up}
        public static EButtonStates ProcessAxisAsButton(string axisName, float threshold, EButtonStates currentState)
        {
            float axisValue = Input.GetAxis(axisName);
            EButtonStates returnState;
            
            if (axisValue < threshold)
            {
                if (currentState == EButtonStates.Pressed)
                {
                    returnState = EButtonStates.Up;
                }
                else
                {
                    returnState = EButtonStates.Off;
                }
            }
            else
            {
                if (currentState == EButtonStates.Off)
                {
                    returnState = EButtonStates.Down;
                }
                else
                {
                    returnState = EButtonStates.Pressed;
                }
            }
            return returnState;
        }
        public class Button
        {
            public MiniStateMachine<NRMInput.EButtonStates> State { get; protected set; }
            public string ButtonID;

            public delegate void ButtonDownMethodDelegate();
            public delegate void ButtonPressedMethodDelegate();
            public delegate void ButtonUpMethodDelegate();

            public ButtonDownMethodDelegate ButtonDownMethod;
            public ButtonPressedMethodDelegate ButtonPressedMethod;
            public ButtonUpMethodDelegate ButtonUpMethod;

            public Button(string playerID, string buttonID, ButtonDownMethodDelegate btnDown, ButtonPressedMethodDelegate btnPressed, ButtonUpMethodDelegate btnUp)
            {
                ButtonID = playerID + "_" + buttonID;
                ButtonDownMethod = btnDown;
                ButtonUpMethod = btnUp;
                ButtonPressedMethod = btnPressed;
                State = new MiniStateMachine<NRMInput.EButtonStates>();
                State.ChangeState(NRMInput.EButtonStates.Off);
            }

            public virtual void TriggerButtonDown()
            {
                ButtonDownMethod();
            }

            public virtual void TriggerButtonPressed()
            {
                ButtonPressedMethod();
            }

            public virtual void TriggerButtonUp()
            {
                ButtonUpMethod();
            }
        }
    }
}
