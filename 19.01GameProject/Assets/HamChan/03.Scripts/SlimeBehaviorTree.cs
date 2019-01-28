using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.AI;

public class SlimeBehaviorTree : MonoBehaviour
{
    private GameObject mOwner;
    private Animator mAnimator;
    private NavMeshAgent mNavMeshAgent;
    private Rigidbody mRigidbody;
    private Vector3 mGoal;
    private BehaviorTree mBehaviorTree;
    private BehaviorTree.Root mRoot;
    private BehaviorTree.Sequence mSequence;
    private BehaviorTree.MoveTo moveTo;
    private FindPlayerService mFindPlayer;
    private BehaviorTree.Wait mWait;
    private BehaviorTree.Selector mMoveToAttack;
    private BehaviorTree.CanAttack mCanAttack;
    private BehaviorTree.CloseAttack mCloseAttack;
    private bool mbMove;
    bool dumy = false;

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
        mRoot = new BehaviorTree.Root();
        mSequence = new BehaviorTree.Sequence();
        mRoot.Child = mSequence;
        moveTo = new BehaviorTree.MoveTo("TargetPlayer");
        mFindPlayer = new FindPlayerService();
        mWait = new BehaviorTree.Wait(0.3f);
        mMoveToAttack = new BehaviorTree.Selector();
        mCanAttack = new BehaviorTree.CanAttack();
        mCloseAttack = new BehaviorTree.CloseAttack();

        //페이즈1
        mSequence.Children.Add(mMoveToAttack);
        mMoveToAttack.Children.Add(mCanAttack);
        mMoveToAttack.Children.Add(moveTo);
        mSequence.Children.Add(mWait);
        mSequence.Children.Add(mCloseAttack);
        mSequence.Children.Add(mWait);
        // mRoot.RunBT();
        mSequence.ServiceList.Add(mFindPlayer);
        BlackBoard.SetValueByVector3Key
                ("CurrentPosition", this.transform.position);

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
}
