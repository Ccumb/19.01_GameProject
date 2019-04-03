using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.Tools;
namespace Neremnem.AI
{
    public enum EBTState
    {
        True,
        False,
        Continue,
        Abort
    }
    public class BehaviorTree
    {
        public static Stack<Node> tickStack;
        public static Animator animator;
        public static GameObject owner;
        public static NavMeshAgent navMeshAgent;
        public BehaviorTree(GameObject ownerObject)
        {
            owner = ownerObject;
            animator = owner.GetComponent<Animator>();
            navMeshAgent = owner.GetComponent<NavMeshAgent>();
        }


        public class Node
        {
            protected Node mparent;
            protected List<Service> mServiceList;
            protected List<Decorator> mDecoratorList;
            protected bool mbLoop;
            protected string mNodeName;
            public bool isLoop
            {
                get { return mbLoop; }
                set { mbLoop = value; }
            }
            public List<Service> ServiceList
            {
                get { return mServiceList; }
            }
            public List<Decorator> DecoratorList
            {
                get { return mDecoratorList; }
            }

            public Node()
            {
                mparent = null;
                mNodeName = "Node";
                mServiceList = new List<Service>();
                mDecoratorList = new List<Decorator>();
            }
            public Node(string name)
            {
                mparent = null;
                mNodeName = name;
                mServiceList = new List<Service>();
                mDecoratorList = new List<Decorator>();
            }
            public string NodeName
            {
                get { return mNodeName; }
                set { mNodeName = value; }
            }
            public virtual EBTState Tick()
            {
                return EBTState.Continue;
            }
        }
        public class Root : Node
        {
            protected Composite mChild;
            public Composite Child
            {
                get { return mChild; }
                set { mChild = value; }
            }
            public Root()
                : base("Root")
            {
                BehaviorTree.tickStack = new Stack<Node>();
            }
            public Root(string nodeName)
                : base(nodeName)
            {
                tickStack = new Stack<Node>();
            }
            public void Tick()
            {
                if (tickStack.Count == 0)
                {
                    tickStack.Push(mChild);
                }
                var temp = tickStack.Pop();
                Debug.Log(temp);
                temp.Tick();
            }
        }
        public class Service
        {
            private float mFuture;
            public List<string> keyList;
            private float mInterval;
            public Service()
            {
                keyList = new List<string>();
                mInterval = 0.5f;
                mFuture = -1;
            }
            public Service(float interval)
            {
                keyList = new List<string>();
                mInterval = interval;
                mFuture = -1;
            }
            public virtual bool Tick()
            {
                if (mFuture < 0)
                {
                    mFuture = Time.time + mInterval;
                    return false; // 아직 실행 안함
                }
                if (Time.time >= mFuture)
                {
                    mFuture = -1;
                    return true; //실행
                }
                else
                {
                    return false;
                }
            }
        }
        public class Decorator
        {

            protected string mNodeName;
            protected Node mParent;
            public Decorator()
            {
                mNodeName = "Decorator";
            }
            public Decorator(string name)
            {
                mNodeName = name;
            }
            public virtual bool Tick()
            {
                return true;
            }
        }
        public class Composite : Node
        {
            protected int mCursor;
            protected EBTState mCompare;
            protected bool bWillAbort;

            public Composite() : base()
            {
                mCursor = -1;
                //mbLoop = false;
            }
            public Composite(string nodeName) : base(nodeName)
            {
                mCursor = -1;
                //mbLoop = false;
            }//
            public bool WillAbort
            {
                get { return bWillAbort; }
                set { bWillAbort = value; }
            }
        }
        //false 만날때까지
        public class Sequence : Composite
        {
            private List<Node> mChildren;
            public List<Node> Children
            {
                get { return mChildren; }
            }

            public Sequence()
            {
                mChildren = new List<Node>();
            }
            public Sequence(string nodeName) : base(nodeName)
            {
                mChildren = new List<Node>();
            }
            public override EBTState Tick()
            {
                if (mServiceList.Count > 0)
                {
                    for (int i = 0; i < mServiceList.Count; i++)
                    {
                        mServiceList[i].Tick();
                    }
                }
                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick() == false)
                        {
                            return EBTState.Abort;
                        }
                    }
                }
                if (mCursor < 0)
                {
                    mCursor = 0;
                }
                switch (mChildren[mCursor].Tick())
                {
                    case EBTState.True:
                        {
                            if (mCursor + 1 < mChildren.Count)
                            {
                                ++mCursor;
                                tickStack.Push(this);
                                return EBTState.Continue;
                            }
                            else
                            {
                                mCursor = -1;
                                return EBTState.True;
                            }
                            break;
                        }
                    case EBTState.False:
                        {
                            mCursor = -1;
                            return EBTState.False;
                        }
                    case EBTState.Continue:
                        {
                            tickStack.Push(this);
                            return EBTState.Continue;
                        }
                    case EBTState.Abort:
                        {
                            mCursor = -1;
                            return EBTState.Abort;
                        }
                }
                return EBTState.Continue;
            }
        }
        //true 만날때까지
        public class Selector : Composite
        {
            private List<Node> mChildren;
            public List<Node> Children
            {
                get
                {
                    return mChildren;
                }
            }
            public Selector()
            {
                mChildren = new List<Node>();
            }
            public Selector(string nodeName) : base(nodeName)
            {
                mChildren = new List<Node>();
            }
            public override EBTState Tick()
            {
                if (mServiceList.Count > 0)
                {
                    for (int i = 0; i < mServiceList.Count; i++)
                    {
                        mServiceList[i].Tick();
                    }
                }
                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick() == false)
                        {
                            Debug.Log("중단되어야댐");
                            return EBTState.Abort;
                        }
                    }
                }
                if (mCursor < 0)
                {
                    mCursor = 0;
                }
                mCompare = mChildren[mCursor].Tick();
                switch (mChildren[mCursor].Tick())
                {
                    case EBTState.True:
                        {
                            mCursor = -1;
                            return EBTState.False;
                        }
                    case EBTState.False:
                        {
                            if (mCursor + 1 < mChildren.Count)
                            {
                                ++mCursor;
                                tickStack.Push(this);
                                return EBTState.Continue;
                            }
                            else
                            {
                                mCursor = -1;
                                return EBTState.True;
                            }
                            break;
                        }
                    case EBTState.Continue:
                        {
                            tickStack.Push(this);
                            return EBTState.Continue;
                        }
                    case EBTState.Abort:
                        {
                            mCursor = -1;
                            return EBTState.Abort;
                        }
                }
                return EBTState.Continue;
            }
        }
        public class Task : Node
        {
            protected string mNodeName;
        }
        public class ImplementRandom : Task
        {
            protected List<Node> mChildren;
            public ImplementRandom(params Node[] children)
            {
                mChildren = new List<Node>();
                for (int i = 0; i < children.Length; i++)
                {
                    mChildren.Add(children[i]);
                }
            }
            public override EBTState Tick()
            {
                tickStack.Push(mChildren[Random.Range(0, mChildren.Count)]);
                return EBTState.True;
            }
        }
        public class MoveTo : Task
        {
            private string mKey;
            private string mCurrentPositionKey;
            private float mStopDistance;
            public float StopDistance
            {
                get { return mStopDistance; }
                set { mStopDistance = value; }
            }
            public void SetBlackBoardKey(string key)
            {
                mKey = key;
            }

            public MoveTo()
            {
                mStopDistance = 3.0f;
                mKey = "Destination";
                mCurrentPositionKey = "CurrentPosition";
            }
            public MoveTo(string key)
            {
                mStopDistance = 3.0f;
                mKey = key;
                mCurrentPositionKey = "CurrentPosition";
            }
            public override EBTState Tick()
            {
                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick()
                            == false)
                        {
                            return EBTState.Abort;
                        }
                    }
                }
                if (mServiceList.Count > 0)
                {
                    for (int i = 0; i < mServiceList.Count; i++)
                    {
                        mServiceList[i].Tick();
                    }
                }
                if (Vector3.Distance
                    (BlackBoard.GetValueByVector3Key(mKey)
                    , BlackBoard.GetValueByVector3Key(mCurrentPositionKey))
                        < mStopDistance)
                {
                    BlackBoard.SetValueByBoolKey("IsMoving", false);
                    animator.SetBool("Walk", false);
                    return EBTState.True;
                }
                else
                {
                    //Debug.Log(" 컨틴유!");
                    BlackBoard.SetValueByBoolKey("IsMoving", true);
                    animator.SetBool("Walk", true);
                    return EBTState.Continue;
                }
            }
        }
        public class Wait : Task
        {
            public float seconds = 0.3f;
            float future = -1;
            public Wait(float seconds)
            {
                NodeName = "Wait";
                this.seconds = seconds;
            }

            public override EBTState Tick()
            {
                if (future < 0)
                {
                    future = Time.time + seconds;
                }
                if (Time.time >= future)
                {
                    future = -1;
                    return EBTState.True;
                }
                else
                {
                    return EBTState.Continue;
                }
            }
        }
        public class AnimationTrigger : Task
        {
            protected Animator mAnimator;
            protected string mTrigger;
            protected int mLayer;
            protected int mID;
            public AnimationTrigger(string trigger, int layer = 0)
            {
                mAnimator = animator;
                mTrigger = trigger;
                mLayer = layer;
            }
            public override EBTState Tick()
            {
                mAnimator.SetTrigger(mTrigger);
                return EBTState.True;
            }
        }
        public class WaitForAnimationEnd : Task
        {
            protected Animator mAnimator;
            protected string mAnimationName;
            protected int mLayer;
            protected int mID;
            WaitForAnimationEnd()
            {
                mAnimator = animator;
                mAnimationName = "Idle";
                mLayer = 0;
                mID = Animator.StringToHash("Idle");
            }
            public override EBTState Tick()
            {
                var state = mAnimator.GetCurrentAnimatorStateInfo(mLayer);
                if (state.fullPathHash == mID || state.shortNameHash == mID)
                {
                    return EBTState.True;
                }
                return EBTState.Continue;
            }
        }
        //public class WaitForAnimation: Task
        //{
        //    protected Animator mAnimator;
        //    protected string mAnimationName;
        //    protected int mLayer;
        //    protected int mID;
        //    WaitForAnimationEnd(string name, int layer = 0)
        //    {
        //        mAnimator = animator;
        //        mAnimationName = name;
        //        mLayer = layer;
        //        mID = Animator.StringToHash(name);
        //    }
        //    public override EBTState Tick()
        //    {
        //        var state = mAnimator.GetCurrentAnimatorStateInfo(mLayer);
        //        if (state.fullPathHash == mID || state.shortNameHash == mID)
        //        {
        //            return EBTState.True;
        //        }
        //        return EBTState.Continue;
        //    }
        //}
        public class CanAttack : Decorator
        {
            private float mRange;
            public CanAttack(string name, float ragne)
            {
                mNodeName = name;
                mRange = ragne;
            }
            public CanAttack(Node parent, float ragne)
            {
                mNodeName = "CanAttack";
                mRange = ragne;
            }
            public override bool Tick()
            {
                //Debug.Log(Vector3.Distance(
                //    BlackBoard.GetValueByVector3Key("TargetPlayer")
                //    , BlackBoard.GetValueByVector3Key("CurrentPosition")));
                //Debug.Log(BlackBoard.GetValueByVector3Key("TargetPlayer"));
                //Debug.Log(BlackBoard.GetValueByVector3Key("CurrentPosition"));
                if (mRange >= Vector3.Distance(
                    BlackBoard.GetValueByVector3Key("TargetPlayer")
                    , BlackBoard.GetValueByVector3Key("CurrentPosition")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public class SetActive : Task
        {
            private GameObject mGameObject;
            private bool mActive;
            public SetActive(GameObject gameObject, bool active)
            {
                mGameObject = gameObject;
                mActive = active;
            }
            public override EBTState Tick()
            {
                mGameObject.SetActive(mActive);
                return EBTState.True;
            }
        }
        public class SpawnSlime : Task
        {
            private int mSpawnCount;
            private bool mbFirstSpawn;
            private bool mbRush;
            public SpawnSlime(string name)
            {
                mNodeName = name;
                mSpawnCount = 0;
                mbFirstSpawn = false;
                mbRush = false;
                BlackBoard.AddIntKey("SlimeAmount", 0);
            }
            public override EBTState Tick()
            {
                if (!mbFirstSpawn)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        EventManager.TriggerCommonEvent("SpawnNormalSlime");
                    }
                    BlackBoard.SetValueByIntKey("SlimeAmount", 3);
                    return EBTState.True;
                }
                else
                {
                    if (mSpawnCount < 5)
                    {
                        if (mbRush == false)
                        {
                            EventManager.TriggerCommonEvent("SpawnNormalSlime");
                            BlackBoard.SetValueByIntKey("SlimeAmount"
                                , BlackBoard.GetValueByIntKey("SlimeAmount") + 1); ;
                        }
                        else
                        {
                            EventManager.TriggerCommonEvent("SpawnRushSlime");
                            BlackBoard.SetValueByIntKey("SlimeAmount"
                                , BlackBoard.GetValueByIntKey("SlimeAmount") + 1); ;
                        }
                    }
                    return EBTState.True;
                }
            }
        }
        public class BashAttack : Task
        {
            public BashAttack()
            {
                mNodeName = "BashAttack";
            }
            public override EBTState Tick()
            {
                base.Tick();

                BlackBoard.SetValueByStringKey("Action", "BashAttack");
                return EBTState.True;
            }
        }
        public class WhirlwindAttack : Task
        {
            public override EBTState Tick()
            {
                base.Tick();

                BlackBoard.SetValueByStringKey("Action", "Whirlwind");
                return EBTState.True;
            }
        }
        public class CheckRoundJudgment : Task
        {
            public CheckRoundJudgment()
            {
                mNodeName = "CheckRoundJudgment";
            }
            public override EBTState Tick()
            {
                base.Tick();

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    if (Vector3.Distance(players[i].transform.position,
                        BlackBoard.GetValueByGameObjectKey("Boss").transform.position)
                        < 5.0f)
                    {
                        EventManager.TriggerTakeDamageEvent("KnockBack", players[i], 1);
                    }
                }
                return EBTState.True;
            }
        }
        public class CheckSectorJudgment : Task
        {
            public CheckSectorJudgment()
            {
                mNodeName = "CheckSectorJudgment";
            }
            public override EBTState Tick()
            {
                base.Tick();

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    if (Mathf.Acos(
                        Vector3.Dot(
                            BlackBoard.GetValueByGameObjectKey("Boss").transform.forward,
                    (players[i].transform.position - BlackBoard.GetValueByGameObjectKey("Boss").transform.position).normalized)
                    ) < 45
                        && Vector3.Distance(BlackBoard.GetValueByGameObjectKey("Boss").transform.position, players[i].transform.position)
                        < 4.0f)
                    {
                        EventManager.TriggerTakeDamageEvent("TakeDamage", players[i], 1);
                    }
                }
                return EBTState.True;
            }
        }
    }
}
