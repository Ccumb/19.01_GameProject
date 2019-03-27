using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.AI;
using Neremnem.Tools;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class SlimeBehaviorTree : MonoBehaviour
{
    private GameObject mGameObject;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody mRigidbody;

    private BehaviorTree mBehaviorTree;
    private BehaviorTree.Root mRoot;
    private BehaviorTree.Sequence mSequence;
    private BehaviorTree.Selector mPhase1;
    private BehaviorTree.Selector mPhase2;
    private BehaviorTree.AnimationTrigger mCloseAttack;

    private void Awake()
    {
        mGameObject = this.gameObject;
        mAnimator = mGameObject.GetComponent<Animator>();
        mNavMeshAgent = mGameObject.GetComponent<NavMeshAgent>();
        mRigidbody = mGameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        mRigidbody.isKinematic = true;
        mNavMeshAgent.SetDestination(BlackBoard.GetValueByVector3Key("Destination"));
        BlackBoard.SetValueByGameObjectKey("Boss", this.gameObject);
        mBehaviorTree = new BehaviorTree(mGameObject);
        SetBehavior();
    }

    private void Update()
    {
        mRoot.Tick();
    }
    private void SetBehavior()
    {

        mRoot = new BehaviorTree.Root("Root");
        mSequence = new BehaviorTree.Sequence("Main");
        mCloseAttack = new BehaviorTree.AnimationTrigger("CloseAttack");
        mCloseAttack.DecoratorList.Add(new BehaviorTree.CanAttack("CanCloseAttack", 3.0f));
        mSequence.Children.Add(mPhase1);
        mPhase1.ServiceList.Add(new FindPlayerService());
        mPhase1.Children.Add(mCloseAttack);
        mPhase1.Children.Add(new BehaviorTree.MoveTo("TargetPlayer"));
    }
}
