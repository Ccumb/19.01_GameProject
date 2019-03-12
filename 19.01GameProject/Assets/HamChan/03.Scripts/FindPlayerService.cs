using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.AI;
public class FindPlayerService : BehaviorTree.Service
{
    private GameObject mTarget;
    public FindPlayerService()
    {
        mTarget = GameObject.Find("Sphere");
    }
    public FindPlayerService(GameObject target)
    {
        mTarget = target;
    }
    public override EBTState Tick()
    {
<<<<<<< HEAD
        Debug.Log(BlackBoard.GetValueByVector3Key("TargetPlayer"));
=======
        //Debug.Log(BlackBoard.GetValueByVector3Key("TargetPlayer"));
>>>>>>> develop
        BlackBoard.SetValueByVector3Key("TargetPlayer"
            , mTarget.transform.position);
        BlackBoard.SetValueByVector3Key("Destination",
            BlackBoard.GetValueByVector3Key("TargetPlayer"));
        return EBTState.True;
    }
}
