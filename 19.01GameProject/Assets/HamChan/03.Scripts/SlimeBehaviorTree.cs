using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.AI;

public class SlimeBehaviorTree : MonoBehaviour
{
    public enum EAnimState
    {
        Bash
        , Whirlwind 
        , Jump 
    }

    private GameObject mOwner;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody mRigidbody;
    private Vector3 mGoal;
    private BehaviorTree mBehaviorTree;
    private BehaviorTree.Root mRoot;
    private BehaviorTree.Sequence mSequence;
    private BehaviorTree.Sequence mPattern1;
    private BehaviorTree.Sequence mPattern2;
    private BehaviorTree.Sequence mPattern2_1;
    private BehaviorTree.Sequence mPattern2_2;
    private BehaviorTree.Sequence mPattern3;
    private BehaviorTree.Sequence mPattern4;

    private BehaviorTree.MoveTo moveTo;
    private FindPlayerService mFindPlayer;
    private BehaviorTree.Wait mWait;
    private BehaviorTree.ImplementRandom mImplementRandom;
    private BehaviorTree.Selector mMoveToAttack;
    private BehaviorTree.CanAttack mCanAttack;
    private BehaviorTree.CloseAttack mCloseAttack;

    private BehaviorTree.ConditionalLoop mLoopPattern1;
    private BehaviorTree.ConditionalLoop mLoopPattern2;
    private BehaviorTree.ConditionalLoop mLoopPattern3;
    private BehaviorTree.ConditionalLoop mLoopPattern4;
    private BehaviorTree.ConditionalLoop mLoopPattern5;
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
        BlackBoard.SetValueByGameObjectKey("Boss", this.gameObject);

        SetBehavior();
    }
    private void Update()
    {
        mbMove = BlackBoard.GetValueByBoolKey("CanMove");
        mRoot.Tick();
        Debug.Log(mbMove);
        if (mbMove)
        {
            mAnimator.SetBool("Move", true);
            mNavMeshAgent.SetDestination
                  (BlackBoard.GetValueByVector3Key("Destination"));
            BlackBoard.SetValueByVector3Key
                ("CurrentPosition", this.transform.position);
            mNavMeshAgent.Resume();

        }
        else
        {
            mNavMeshAgent.Stop();
            mAnimator.SetBool("Move", false);
        }
        if (BlackBoard.GetValueByBoolKey("Attack"))
        {
            mAnimator.SetTrigger("Attack");
            BlackBoard.SetValueByBoolKey("Attack", false);
            IEnumerator WaitForAnimation(Animation animation)
            {
                do
                {
                    yield return null;
                } while (animation.isPlaying);
            }
        }
    }
    private void SetBehavior()
    {
      
        mBehaviorTree = new BehaviorTree();
        mRoot = new BehaviorTree.Root();
        mSequence = new BehaviorTree.Sequence();
        mPattern1 = new BehaviorTree.Sequence("Pattern 1");
        mPattern2 = new BehaviorTree.Sequence("Pattern 1");
        mPattern2_1 = new BehaviorTree.Sequence("Pattern 2_1");
        mPattern2_2 = new BehaviorTree.Sequence("Pattern 2_2");
        mPattern3 = new BehaviorTree.Sequence("Pattern 3");
        mPattern4 = new BehaviorTree.Sequence("Pattern 4");

        mRoot.Child = mSequence;
        moveTo = new BehaviorTree.MoveTo("TargetPlayer");
        mFindPlayer = new FindPlayerService();
        mWait = new BehaviorTree.Wait(0.3f);
        mMoveToAttack = new BehaviorTree.Selector();
        mCanAttack = new BehaviorTree.CanAttack();
        mCloseAttack = new BehaviorTree.CloseAttack();

        //루프 데코레이터
        mLoopPattern1 = new BehaviorTree.ConditionalLoop("Phase1", true, mPattern1);
        mLoopPattern2 = new BehaviorTree.ConditionalLoop("Phase2", true, mPattern2_1);
        mLoopPattern3 = new BehaviorTree.ConditionalLoop("Phase3", true, mPattern3);
        mLoopPattern4 = new BehaviorTree.ConditionalLoop("Phase4", true, mPattern4);
        mLoopPattern4 = new BehaviorTree.ConditionalLoop("Phase5", true, mPattern4);

        //mImplementRandom = new BehaviorTree.ImplementRandom(mPattern2_1,mPattern2_2);

        //메인 스트림
        mSequence.ServiceList.Add(mFindPlayer);
        BlackBoard.SetValueByVector3Key
                ("CurrentPosition", this.transform.position);
        mSequence.Children.Add(mPattern1);
        mSequence.Children.Add(mImplementRandom);

        //페이즈1
        mPattern1.DecoratorList.Add(mLoopPattern1);
        mPattern1.Children.Add(mMoveToAttack);
        mMoveToAttack.Children.Add(mCanAttack);
        mMoveToAttack.Children.Add(moveTo);
        mPattern1.Children.Add(mWait);
        mPattern1.Children.Add(mCloseAttack);
        mPattern1.Children.Add(mWait);

        //페이즈2
        mPattern2.Children.Add(mMoveToAttack);
        mPattern2.Children.Add(mWait);
        mPattern2.Children.Add(new BehaviorTree.BashAttack());
        mPattern2.Children.Add(new BehaviorTree.CheckSectorJudgment());

        //장판 그리고
        //공격
        mPattern2_2.Children.Add(new BehaviorTree.Wait(0.5f));
    }
}
