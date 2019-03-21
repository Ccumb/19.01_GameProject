using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템이나 스킬을 이동시킬 수 있게 해주는 인터페이스
/// </summary>
public interface IMoveable
{
    Sprite MyIcon
    {
        get;
    }
}
