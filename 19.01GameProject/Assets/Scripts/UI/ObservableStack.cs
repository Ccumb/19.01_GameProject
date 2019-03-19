﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;
    public event UpdateStackEvent OnPeek;

    public new void Push(T item)
    {
        base.Push(item);

        if(OnPush != null)
        {
            OnPush();
        }
    }

    public new T Pop()
    {
        T item = base.Pop();

        if(OnPop != null)
        {
            OnPop();
        }

        return item;
    }

    public new void Clear()
    {
        base.Clear();

        if(OnClear != null)
        {
            OnClear();
        }
    }

    public new T Peek()
    {
        T item = base.Peek();

        if(OnPeek != null)
        {
            OnPeek();
        }
        return item;
    }

    public ObservableStack(ObservableStack<T> items) : base(items)
    {

    }
    
    public ObservableStack()
    {

    }

}
