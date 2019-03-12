using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator mAnimator;
    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    public void AlertAnimationEnd()
    {

    }
}
