using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Neremnem.Tools
{
    public class MiniStateMachine<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public T CurrentState { get; protected set; }
        public T PreviousState { get; protected set; }
        
        public virtual void ChangeState(T newState)
        {
            if (newState.Equals(CurrentState))
            {
                return;
            }

            PreviousState = CurrentState;
            CurrentState = newState;
        }
        
        public virtual void RestorePreviousState()
        {
            CurrentState = PreviousState;
        }
    }
}