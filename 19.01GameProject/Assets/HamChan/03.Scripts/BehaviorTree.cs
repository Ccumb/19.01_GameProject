using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    //IEnumerator Run()
    //{
    //    yield return new WaitForSeconds(0.5f); 
    //}
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
                mNodeName = null;
                mbLoop = false;
                mServiceList = new List<Service>();
                mDecoratorList = new List<Decorator>();
            }
            public Node(string name)
            {
                mNodeName = name;
                mbLoop = false;
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
                : base()
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
                    Debug.Log("추가");
                    Debug.Log(mChild);
                }
                mTickStack.Pop().Tick();
                //temp.Tick();
            }
            public void RunBT()
            {
                Node temp;
                while (true)
                {
                    if (mTickStack.Count == 0)
                    {
                        mTickStack.Push(mChild);
                    }
                    temp = mTickStack.Pop();
                    temp.Tick();
                }
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
                mInterval = interval;
                mFuture = -1;
            }
            public override EBTState Tick()
            {
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
            private Node mParent;
            public Decorator()
            {
                mNodeName = "Decorator";
            }
            public Decorator(string name)
            {
                mNodeName = name;
            }
        }
        public class ConditionalLoop : Decorator
        {
            private bool mbSet;
            private string mKey;
            private Node mParent;
            public bool IsSet
            {
                get { return mbSet; }
                set { mbSet = value; }
            }
            public string Key
            {
                get { return mKey; }
                set { mKey = value; }
            }
            public ConditionalLoop(string key, bool offset, Node parent)
                : base("Conditional Loop")
            {
                mbSet = offset;
                mKey = key;
                mParent = parent;
            }
            public ConditionalLoop(string name, string key,bool offset, Node parent)
                : base(name)
            {
                mbSet = offset;
                mKey = key;
                mParent = parent;
            }
            public override EBTState Tick()
            {
                if (mbSet)
                {
                    if (BlackBoard.GetValueByStringKey(mKey) != null)
                    {
                        mParent.isLoop = true;
                    }
                    else
                    {
                        mParent.isLoop = false;
                    }
                }
                else // is not set
                {
                    if (BlackBoard.GetValueByStringKey(mKey) == null)
                    {
                        mParent.isLoop = true;
                    }
                    else
                    {
                        mParent.isLoop = false;
                    }
                }
                return EBTState.True;
            }
        }
        public class Composite : Node
        {
            protected int mCursor;
            protected EBTState mCompare;
            public Composite() : base()
            {
                mCursor = -1;
                mbLoop = false;
            }
            public Composite(string nodeName) : base(nodeName)
            {
                mCursor = -1;
                mbLoop = false;
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
                        if (mServiceList[i].Tick() == EBTState.False)
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
                            return EBTState.False;
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
                        mTickStack.Push(mChildren[++mCursor]);
                    }
                    else
                    {
                        if (mbLoop)
                        {
                            mCursor = -1;
                            mTickStack.Push(this);
                            return EBTState.True;
                        }
                        mCursor = -1;
                        return EBTState.True;
                    }
                }
                else if (mCompare == EBTState.False)
                {
                    if (mbLoop)
                    {
                        mCursor = -1;
                        mTickStack.Push(this);
                        return EBTState.True;
                    }
                    mCursor = -1;
                    return EBTState.False;

                }
                else if (mCompare == EBTState.Continue)
                {
                    mTickStack.Push(mChildren[mCursor]);
                    return EBTState.Continue;
                }
                return EBTState.Running;
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
                if (mCursor < 0)
                {
                    mCursor = 0;
                }
                mCompare = mChildren[mCursor].Tick();

                if (mCompare
                    == EBTState.False)
                {
                    if (mCursor + 1 < mChildren.Count)
                    {
                        mTickStack.Push(mChildren[++mCursor]);
                    }
                    else
                    {
                        if (mbLoop)
                        {
                            mCursor = -1;
                            mTickStack.Push(this);
                            return EBTState.True;
                        }
                        mCursor = -1;
                        return EBTState.False;
                    }
                }
                else if (mCompare
                    == EBTState.True)
                {
                    if (mbLoop)
                    {
                        mCursor = -1;
                        mTickStack.Push(this);
                        return EBTState.True;
                    }
                    mCursor = -1;
                    return EBTState.True;
                }
                else if (mCompare
                    == EBTState.Continue)
                {
                    mTickStack.Push(mChildren[mCursor]);
                    return EBTState.Continue;
                }
                return EBTState.Running;
            }
        }
        public class Task : Node
        {

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
                        < 1.0f)
                {
                    //Debug.Log("true!");

                    BlackBoard.SetValueByBoolKey("CanMove", false);
                    return EBTState.True;
                }
                else
                {
                    Debug.Log(" 컨틴유!");
                    BlackBoard.SetValueByBoolKey("CanMove", true);
                    return EBTState.Continue;
                }
            }
        }
        public class Wait : Node
        {
            public float seconds = 0.3f;
            float future = -1;
            public Wait(float seconds)
            {
                this.seconds = seconds;
            }

            public override EBTState Tick()
            {
                Debug.Log("wait 호출");
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
        public class CanAttack : Node
        {
            private float mRange = 0.5f;
            public override EBTState Tick()
            {
                Debug.Log(Vector3.Distance(
                    BlackBoard.GetValueByVector3Key("TargetPlayer")
                    , BlackBoard.GetValueByVector3Key("CurrentPosition")));
                Debug.Log(BlackBoard.GetValueByVector3Key("TargetPlayer"));
                Debug.Log(BlackBoard.GetValueByVector3Key("CurrentPosition"));
                if (mRange >= Vector3.Distance(
                    BlackBoard.GetValueByVector3Key("TargetPlayer")
                    , BlackBoard.GetValueByVector3Key("CurrentPosition")))
                {
                    Debug.Log("treu");
                    return EBTState.True;
                }
                else
                {
                    Debug.Log("false");
                    return EBTState.False;
                }
            }
        }
        public class CloseAttack : Node
        {
            public override EBTState Tick()
            {
                BlackBoard.SetValueByBoolKey("Attack", true);
                return EBTState.True;
            }
        }


    }


}
