using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.AI;
<<<<<<< HEAD
=======
using Neremnem.Tools;
>>>>>>> develop

public class SlimeBehaviorTree : MonoBehaviour
{
    public enum EAnimState
    {
        Bash
        , Whirlwind
        , Jump
<<<<<<< HEAD
        , Spawn
    }

    private string mAction;
    private bool mAnimationEnd = false;
=======
    }

    private string mAction;
    private bool mbIsActionEnd = true;
>>>>>>> develop
    private GameObject mOwner;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody mRigidbody;
    private BehaviorTree mBehaviorTree;
    private BehaviorTree.Root mRoot;
    private BehaviorTree.Sequence mSequence;
    private BehaviorTree.Sequence mPhase1;
    private BehaviorTree.Selector mPhase2;
    private BehaviorTree.Sequence mPhase3;
    private BehaviorTree.Sequence mPhase4;
    private BehaviorTree.Sequence mPhase5;

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
    private BehaviorTree.Selector mMoveToAttack2;
    private BehaviorTree.CanAttack mCanAttack;
    private BehaviorTree.CloseAttack mCloseAttack;
<<<<<<< HEAD

    private BehaviorTree.ConditionalLoop mLoopPattern1;
    private BehaviorTree.ConditionalLoop mLoopPattern2;
    private BehaviorTree.ConditionalLoop mLoopPattern3;
    private BehaviorTree.ConditionalLoop mLoopPattern4;
    private BehaviorTree.ConditionalLoop mLoopPattern5;
=======
    
>>>>>>> develop
    
    // private BehaviorTree.Blackboard mCheckHP;


    private bool mbMove;

    private void Awake()
    {
        mOwner = this.gameObject;
        mAnimator = mOwner.GetComponent<Animator>();
        mNavMeshAgent = mOwner.GetComponent<NavMeshAgent>();
        mRigidbody = mOwner.GetComponent<Rigidbody>();
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
        BlackBoard.SetValueByVector3Key
                ("CurrentPosition", this.transform.position);
<<<<<<< HEAD
        mRoot.Tick();
        mAction = BlackBoard.GetValueByStringKey("Action");
        if (BlackBoard.GetValueByStringKey("Action") != null)
        {
            mAnimationEnd = false;
            mAnimator.SetTrigger(mAction);
            
            //StartCoroutine(WaitForAnimation());
            BlackBoard.SetValueByStringKey("Action", null);
        }
        mbMove = BlackBoard.GetValueByBoolKey("CanMove");
        if (mbMove)
=======
        if(mbIsActionEnd == true)
        {
            mRoot.Tick();
        }
        if (BlackBoard.GetValueByStringKey("Action") != null)
        {
            EventManager.TriggerCommonEvent("ChangeAttackColor");
            mbIsActionEnd = false;
            mAnimator.SetTrigger("Attack");            
            BlackBoard.SetValueByStringKey("Action", null);
        }
        mbMove = BlackBoard.GetValueByBoolKey("CanMove");
        if (mbMove == true
            && mbIsActionEnd == true)
>>>>>>> develop
        {
            mAnimator.SetBool("Move", true);
            mNavMeshAgent.SetDestination
                  (BlackBoard.GetValueByVector3Key("Destination"));
<<<<<<< HEAD

=======
>>>>>>> develop
            mNavMeshAgent.Resume();

        }
        else
        {
            mNavMeshAgent.Stop();
            mAnimator.SetBool("Move", false);
        }

    }
    private void SetBehavior()
    {

<<<<<<< HEAD
        mRoot = new BehaviorTree.Root();
=======
        mRoot = new BehaviorTree.Root("Root");
>>>>>>> develop
        mSequence = new BehaviorTree.Sequence("Main");
        
        mPattern2_1 = new BehaviorTree.Sequence("Pattern 2_1");
        mPattern2_2 = new BehaviorTree.Sequence("Pattern 2_2");
        

        mPhase1 = new BehaviorTree.Sequence("Phase1");
        mPhase2 = new BehaviorTree.Selector("Phase2");

        mRoot.Child = mSequence;
        moveTo = new BehaviorTree.MoveTo("TargetPlayer");
        mFindPlayer = new FindPlayerService();
        mWait = new BehaviorTree.Wait(0.3f);
        mMoveToAttack = new BehaviorTree.Selector("moveToAttack");
        mMoveToAttack2 = new BehaviorTree.Selector("moveToAttack");
        mCanAttack = new BehaviorTree.CanAttack();
        mCloseAttack = new BehaviorTree.CloseAttack();


        //메인 스트림
        BlackBoard.SetValueByVector3Key
                ("CurrentPosition", this.transform.position);
<<<<<<< HEAD
        //mSequence.Children.Add(mPhase1);
        mSequence.Children.Add(mPhase2);
        mSequence.ServiceList.Add(mFindPlayer);
        //페이즈1
        mPhase1.DecoratorList.Add(new BehaviorTree.CompareString("Phase"
            , BehaviorTree.Decorator.EObserverAborts.Self
=======
        mSequence.Children.Add(mPhase1);
        mSequence.Children.Add(mPattern2_1);
        mSequence.Children.Add(mPattern2_2);
        //mSequence.Children.Add(mPhase2);
        //페이즈1
        mPhase1.DecoratorList.Add(new BehaviorTree.CompareString("Phase"
            , EObserverAborts.Self
>>>>>>> develop
            ,"Phase1"));
        mPhase1.ServiceList.Add(mFindPlayer);
        mPhase1.Children.Add(mMoveToAttack);
        mMoveToAttack.Children.Add(mCanAttack);
        mMoveToAttack.Children.Add(moveTo);
        mPhase1.Children.Add(new BehaviorTree.Wait(0.3f));
        mPhase1.Children.Add(mCloseAttack);
        mPhase1.Children.Add(new BehaviorTree.Wait(0.3f));
        //애니메이션에 이벤트 넣어서 각각 시작, 끝날때 공격 체크하자
<<<<<<< HEAD
        mMoveToAttack2 = mMoveToAttack;

        //페이즈2       
        mPhase2.DecoratorList.Add(new BehaviorTree.CompareString("Phase"
            , BehaviorTree.Decorator.EObserverAborts.Self
            , "Phase2"));
        mPhase2.ServiceList.Add(mFindPlayer);
        mPhase2.Children.Add(mPattern2_1);
        mPhase2.Children.Add(mPattern2_2);
        mPattern2_2.Children.Add(mMoveToAttack);
=======

        //페이즈2       
        mPhase2.DecoratorList.Add(new BehaviorTree.CompareString("Phase"
            , EObserverAborts.Self
            , "Phase2"
            , "2페이즈 데코레이터"));
        //mPhase2.ServiceList.Add(mFindPlayer);
        mPhase2.Children.Add(mPattern2_1);
        mPhase2.Children.Add(mPattern2_2);
        mPattern2_2.Children.Add(mMoveToAttack);
        mPattern2_2.ServiceList.Add(mFindPlayer);
>>>>>>> develop
        //mPattern2_1.Children.Add('장판');
        mPattern2_2.Children.Add(mWait);
        mPattern2_2.Children.Add(new BehaviorTree.BashAttack());
        mPattern2_2.Children.Add(new BehaviorTree.CheckSectorJudgment());

        //장판 그리고
        //공격
        //mPattern2_2.Children.Add('장판');
        mPattern2_1.DecoratorList.Add(new BehaviorTree.CompareKey
<<<<<<< HEAD
            ("Whirlwind", BehaviorTree.Decorator.EObserverAborts.Self,true));
=======
            ("Whirlwind", EObserverAborts.Self,true));
>>>>>>> develop
        mPattern2_1.Children.Add(new BehaviorTree.Wait(0.5f));
        mPattern2_1.Children.Add(new BehaviorTree.WhirlwindAttack());
        mPattern2_1.Children.Add(new BehaviorTree.CheckRoundJudgment());

<<<<<<< HEAD
        mPhase3.Children.Add(new BehaviorTree.SpawnSlime("SpawnSoldiers"));
    }
    public void ActionEnd()
    {
        Debug.Log("ㅁㄴㅇㄹ");
        mAnimationEnd = true;
        mAction = null;

=======
        //mPhase3.Children.Add(new BehaviorTree.SpawnSlime("SpawnSoldiers"));
    }
    public void ActionEnd()
    {
        new WaitForSeconds(0.1f);
        EventManager.TriggerCommonEvent("ChangeNormalColor");
        mbIsActionEnd = true;
>>>>>>> develop
    }
}
