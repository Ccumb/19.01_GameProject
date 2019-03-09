using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Neremnem.Tools;
namespace Neremnem.AI
{
    public enum EBTState
    {
        Running,
        True,
        False,
        Continue,
        Abort
    }
    public enum EObserverAborts
    {
        None,
        Self,
        LowerPriority,
        Both
    }
    public class BehaviorTree
    {
        public static Stack<Node> mTickStack;
        public BehaviorTree()
        {
        }
        public class Node
        {
            protected List<Service> mServiceList;
            protected List<Decorator> mDecoratorList;
            //protected bool mbLoop;
            protected string mNodeName;
            public EObserverAborts AbortOption;
            //public bool isLoop
            //{
            //    get { return mbLoop; }
            //    set { mbLoop = value; }
            //}
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
                mNodeName = "Node";
                //mbLoop = false;
                mServiceList = new List<Service>();
                mDecoratorList = new List<Decorator>();
                AbortOption = EObserverAborts.None;
            }
            public Node(string name)
            {
                mNodeName = name;
                //mbLoop = false;
                mServiceList = new List<Service>();
                mDecoratorList = new List<Decorator>();
                AbortOption = EObserverAborts.None;
            }
            public string NodeName
            {
                get { return mNodeName; }
                set { mNodeName = value; }
            }
            public virtual EBTState Tick()
            {
                //Debug.Log(NodeName);
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
                mTickStack = new Stack<Node>();
            }
            public Root(string nodeName)
                : base(nodeName)
            {
                mTickStack = new Stack<Node>();
            }
            public void Tick()
            {
                if (mTickStack.Count == 0)
                {
                    mTickStack.Push(mChild);
                    //Debug.Log("추가");
                    Debug.Log(mTickStack.Peek().NodeName);
                }
                mTickStack.Pop().Tick();
                //temp.Tick();
            }            
        }
        public class Service : Node
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
            public virtual EBTState Tick()
            {
                base.Tick();
                if (mFuture < 0)
                {
                    mFuture = Time.time + mInterval;
                }
                if (Time.time >= mFuture)
                {
                    mFuture = -1;
                    return EBTState.True;
                }
                else
                {
                    return EBTState.Continue;
                }
            }
        }
        public class Decorator : Node
        {
            
            private string mNodeName;
            protected Node mParent;
            public Decorator()
            {
                mNodeName = "Decorator";
            }
            public Decorator(string name)
            {
                mNodeName = name;
            }
        }
        public class CompareString : Decorator
        {
            private string mKey;
            private string mCompare;
            public CompareString(string key, EObserverAborts option, string compare, string name)
                : base(name)
            {
                mKey = key;
                AbortOption = option;
                mCompare = compare;
            }
            public CompareString(string key, EObserverAborts option, string compare)
            {
                mKey = key;
                AbortOption = option;
                mCompare = compare;
            }
            public override EBTState Tick()
            {
                Debug.Log(NodeName);
                //Debug.Log(mKey + " " + BlackBoard.GetValueByBoolKey(mKey));
                //is set
                Debug.Log(BlackBoard.GetValueByStringKey(mKey));
                Debug.Log(mCompare);
                Debug.Log(BlackBoard.GetValueByStringKey(mKey) == mCompare);

                if (mCompare == BlackBoard.GetValueByStringKey(mKey))
                {
                    return EBTState.True;
                }
                else
                {
                    return EBTState.Abort;
                }
            }
        }
        public class CompareKey : Decorator
        {
            private string mKey;
            private EObserverAborts mObserverAborts;
            private object mCompare;
            public CompareKey(string key, EObserverAborts option, object compare, string name)
                : base(name)
            {
                mKey = key;
                mObserverAborts = option;
                mCompare = compare;
            }
            public CompareKey(string key, EObserverAborts option, object compare)
            {
                mKey = key;
                mObserverAborts = option;
                mCompare = compare;
            }
            public override EBTState Tick()
            {
                //Debug.Log(mKey + " " + BlackBoard.GetValueByBoolKey(mKey));
                //is set

                if (mCompare == BlackBoard.GetValue(mKey))
                {
                    return EBTState.True;
                }
                else
                {
                    return EBTState.Abort;
                }
            }
        }

        //public class ConditionalLoop : Decorator
        //{
        //    private bool mbSet;
        //    private string mKey;
        //    public bool IsSet
        //    {
        //        get { return mbSet; }
        //        set { mbSet = value; }
        //    }
        //    public string Key
        //    {
        //        get { return mKey; }
        //        set { mKey = value; }
        //    }
        //    public ConditionalLoop(string key, bool offset, Node parent)
        //        : base("Conditional Loop")
        //    {
        //        mbSet = offset;
        //        mKey = key;
        //        mParent = parent;
        //    }
        //    public ConditionalLoop(string name, string key, bool offset, Node parent)
        //        : base(name)
        //    {
        //        mbSet = offset;
        //        mKey = key;
        //        mParent = parent;
        //    }
        //    public override EBTState Tick()
        //    {
        //        base.Tick();

        //        if (mbSet)
        //        {
        //            if (BlackBoard.GetValueByBoolKey(mKey) != null)
        //            {
        //                mParent.isLoop = true;
        //            }
        //            else
        //            {
        //                mParent.isLoop = false;
        //            }
        //        }
        //        else // is not true
        //        {
        //            if (BlackBoard.GetValueByBoolKey(mKey) == null)
        //            {
        //                mParent.isLoop = false;
        //            }
        //            else
        //            {
        //                mParent.isLoop = true;
        //            }
        //        }
        //        return EBTState.True;
        //    }
        //}
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
                base.Tick();

                if (mServiceList.Count > 0)
                {
                    for (int i = 0; i < mServiceList.Count; i++)
                    {
                        if (mServiceList[i].Tick() == EBTState.False)
                        {
                            Debug.Log("false");
                            return EBTState.False;
                        }
                    }
                }
                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick() == EBTState.Abort)
                        {
                            AbortOption = mDecoratorList[i].AbortOption;
                            return EBTState.Abort;
                        }
                    }
                }
                if (mCursor < 0)
                {
                    mCursor = 0;
                }              
                mCompare = mChildren[mCursor].Tick();                
                if (mCompare == EBTState.True)
                {
                    if (mCursor + 1 < mChildren.Count)
                    {
                        ++mCursor;
                    }
                    else
                    {
                        mCursor = -1;
                        return EBTState.True;
                    }
                }
                else if (mCompare == EBTState.False)
                {
                    
                    mCursor = -1;
                    return EBTState.False;

                }
                else if (mCompare == EBTState.Continue)
                {
                    //mTickStack.Push(this);
                    return EBTState.Continue;
                }
                else if (mCompare == EBTState.Abort)
                {
                    switch (AbortOption)
                    {
                        case EObserverAborts.Both:
                            {
                                break;
                            }
                        case EObserverAborts.LowerPriority:
                            {
                                break;
                            }
                        case EObserverAborts.Self:
                            {
                                if (mCursor + 1 < mChildren.Count)
                                {
                                    ++mCursor;
                                }
                                else
                                {
                                    mCursor = -1;
                                }
                                break;
                            }
                    }
                    return EBTState.Abort;
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
                        if (mServiceList[i].Tick()
                            == EBTState.False)
                        {
                            return EBTState.False;
                        }
                    }
                }
                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick() == EBTState.False)
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
                Debug.Log(NodeName +":" + mCompare);

                if (mCompare
                    == EBTState.False)
                {
                    if (mCursor + 1 < mChildren.Count)
                    {
                        mTickStack.Push(mChildren[++mCursor]);
                    }
                    else
                    {
                        //if (mbLoop)
                        //{
                        //    mCursor = -1;
                        //    mTickStack.Push(this);
                        //    return EBTState.True;
                        //}
                        mCursor = -1;
                        return EBTState.False;
                    }
                }
                else if (mCompare
                    == EBTState.True)
                {
                    //if (mbLoop)
                    //{
                    //    mCursor = -1;
                    //    mTickStack.Push(this);
                    //    return EBTState.True;
                    //}
                    mCursor = -1;
                    return EBTState.True;
                }
                else if (mCompare
                    == EBTState.Continue)
                {
                    mTickStack.Push(mChildren[mCursor]);
                    return EBTState.Continue;
                }
                else if (mCompare == EBTState.Abort)
                {
                    switch (mChildren[mCursor].AbortOption)
                    {
                        case EObserverAborts.Both:
                            {
                                break;
                            }
                        case EObserverAborts.LowerPriority:
                            {
                                break;
                            }
                        case EObserverAborts.Self:
                            {
                                if (mCursor + 1 < mChildren.Count)
                                {
                                    mTickStack.Push(mChildren[++mCursor]);
                                }
                                else
                                {
                                    mCursor = -1;
                                }
                                break;
                            }
                    }
                    return EBTState.Abort;
                }
                return EBTState.Running;
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
                mTickStack.Push(mChildren[Random.Range(0, mChildren.Count)]);
                return EBTState.True;
            }
        }
        public class MoveTo : Task
        {
            private string mKey;
            private string mCurrentPositionKey;
            public void SetBlackBoardKey(string key)
            {
                mKey = key;
            }

            public MoveTo()
            {
                mKey = "Destination";
                mCurrentPositionKey = "CurrentPosition";
            }
            public MoveTo(string key)
            {
                mKey = key;
                mCurrentPositionKey = "CurrentPosition";
            }
            public override EBTState Tick()
            {
                base.Tick();

                if (mDecoratorList.Count > 0)
                {
                    for (int i = 0; i < mDecoratorList.Count; i++)
                    {
                        if (mDecoratorList[i].Tick()
                            == EBTState.Abort)
                        {
                            return EBTState.Abort;
                        }
                    }
                }
                if (mServiceList.Count > 0)
                {
                    for (int i = 0; i < mServiceList.Count; i++)
                    {
                        if (mServiceList[i].Tick()
                            == EBTState.False)
                        {
                            return EBTState.False;
                        }
                    }
                }
                //Debug.Log(Vector3.Distance
                //    (BlackBoard.GetValueByVector3Key(mKey)
                //    , BlackBoard.GetValueByVector3Key(mCurrentPositionKey)));
                if (Vector3.Distance
                    (BlackBoard.GetValueByVector3Key(mKey)
                    , BlackBoard.GetValueByVector3Key(mCurrentPositionKey))
                        < 3.0f)
                {
                    //Debug.Log("true!");

                    BlackBoard.SetValueByBoolKey("CanMove", false);
                    return EBTState.True;
                }
                else
                {
                    //Debug.Log(" 컨틴유!");
                    BlackBoard.SetValueByBoolKey("CanMove", true);
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
        public class CanAttack : Task
        {
            private float mRange;
            public CanAttack()
            {
                mNodeName = "CanAttack";
                mRange = 3f;
            }
            public override EBTState Tick()
            {
                base.Tick();

                //Debug.Log(Vector3.Distance(
                //    BlackBoard.GetValueByVector3Key("TargetPlayer")
                //    , BlackBoard.GetValueByVector3Key("CurrentPosition")));
                //Debug.Log(BlackBoard.GetValueByVector3Key("TargetPlayer"));
                //Debug.Log(BlackBoard.GetValueByVector3Key("CurrentPosition"));
                if (mRange >= Vector3.Distance(
                    BlackBoard.GetValueByVector3Key("TargetPlayer")
                    , BlackBoard.GetValueByVector3Key("CurrentPosition")))
                {
                    //Debug.Log("true");
                    return EBTState.True;
                }
                else
                {
                    //Debug.Log("false");
                    return EBTState.False;
                }
            }
        }
        public class SetActive : Task
        {
            private GameObject mTarget;
            public SetActive(GameObject target)
            {
                mTarget = target;
            }
            public override EBTState Tick()
            {
                base.Tick();

                mTarget.SetActive(true);
                return EBTState.True;
            }
        }
        public class SetNotActive : Task
        {
            private GameObject mTarget;
            public SetNotActive(GameObject target)
            {
                mTarget = target;
            }
            public override EBTState Tick()
            {
                base.Tick();

                mTarget.SetActive(false);
                return EBTState.True;
            }
        }
        public class SetEnable : Task
        {
            public SetEnable()
            {
                mNodeName = "SetEnable";
            }
            private string mTarget;
            public SetEnable(string target)
            {
                mTarget = target;
            }
            public override EBTState Tick()
            {
                base.Tick();

                EventManager.TriggerCommonEvent(mTarget);
                return EBTState.True;
            }
        }
        public class SetDisable : Task
        {
            private string mTarget;
            public SetDisable(string target)
            {
                mTarget = target;
            }
            public override EBTState Tick()
            {
                base.Tick();

                EventManager.TriggerCommonEvent(mTarget);
                return EBTState.True;
            }
        }

        public class CloseAttack : Task
        {
            public override EBTState Tick()
            {
                base.Tick();

                // Debug.Log("CloseAttack");
                BlackBoard.SetValueByStringKey("Action", "NormalAttack");
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
