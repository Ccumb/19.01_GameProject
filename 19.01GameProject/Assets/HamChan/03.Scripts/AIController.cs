using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class AIController :MonoBehaviour
{
    private GameObject mOwner;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody mRigidbody;
    private Vector3 mGoal;
    private BehaviorTree mBehaviorTree;
    private BehaviorTree.Root root;
    private BehaviorTree.Sequence sequence;
    private BehaviorTree.MoveTo moveTo;
    private bool mbMove;
    private void Awake()
    {
        mOwner = this.gameObject;
        mAnimator = mOwner.GetComponent<Animator>();
        mNavMeshAgent = mOwner.GetComponent<NavMeshAgent>();
        mRigidbody = mOwner.GetComponent<Rigidbody>();
        mGoal = mOwner.transform.position;
    }
    private void Start()
    {
        mRigidbody.isKinematic = true;
        mNavMeshAgent.SetDestination(BlackBoard.GetValueByVector3Key("Destination"));
        mBehaviorTree = new BehaviorTree();
        root = new BehaviorTree.Root();
        sequence = new BehaviorTree.Sequence();
        root.Child = sequence;
        moveTo = new BehaviorTree.MoveTo();
        BlackBoard.SetValueByVector3Key("Destination", new Vector3(0f,0f,10f));

        sequence.Children.Add(moveTo);
        
    }
    private void Update()
    {
        
    }

}
