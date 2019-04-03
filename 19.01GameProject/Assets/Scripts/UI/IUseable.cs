using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
    int MyCost
    {
        get;
        set;
    }
    void Use();
}
