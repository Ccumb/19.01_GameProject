using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Neremnem.Tools { 
public class CommonEvent : UnityEvent { }
public class StringEvent : UnityEvent<string> { }
public class IntEvent : UnityEvent<int> { }
public class FloatEvent : UnityEvent<float> { }
public class GameObjectEvent : UnityEvent<GameObject> { }
public class TransformEvent : UnityEvent<Transform> { }
public class TakeDamageEvent : UnityEvent<GameObject, int> { }
    public class EventManager
    {
        private static Dictionary<string, CommonEvent> mEventDictionary;
        private static Dictionary<string, IntEvent> mIntEventDictionary;
        private static Dictionary<string, FloatEvent> mFloatEventDictionary;
        private static Dictionary<string, StringEvent> mStringEventictionary;
        private static Dictionary<string, GameObjectEvent> mGameobjectEventDictionary;
        private static Dictionary<string, TransformEvent> mTransformEventDictionary;
        private static Dictionary<string, TakeDamageEvent> mTakeDamageEventDictionary;

        public static void Init()
        {
            if (mEventDictionary == null)
            {
                mEventDictionary = new Dictionary<string, CommonEvent>();
            }
            if (mStringEventictionary == null)
            {
                mStringEventictionary = new Dictionary<string, StringEvent>();
            }
            if (mIntEventDictionary == null)
            {
                mIntEventDictionary = new Dictionary<string, IntEvent>();
            }
            if (mFloatEventDictionary == null)
            {
                mFloatEventDictionary = new Dictionary<string, FloatEvent>();
            }
            if (mTransformEventDictionary == null)
            {
                mTransformEventDictionary = new Dictionary<string, TransformEvent>();
            }
            if (mGameobjectEventDictionary == null)
            {
                mGameobjectEventDictionary = new Dictionary<string, GameObjectEvent>();
            }


        }
        public static void StartListeningCommonEvent(string eventName, UnityAction lisnter)
        {
            Init();
            CommonEvent thisEvent = null;
            if (mEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new CommonEvent();
                thisEvent.AddListener(lisnter);
                mEventDictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningStringEvent(string eventName, UnityAction<string> lisnter)
        {
            Init();
            StringEvent thisEvent = null;
            if (mStringEventictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new StringEvent();
                thisEvent.AddListener(lisnter);
                mStringEventictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningIntEvent(string eventName, UnityAction<int> lisnter)
        {
            Init();
            IntEvent thisEvent = null;
            if (mIntEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new IntEvent();
                thisEvent.AddListener(lisnter);
                mIntEventDictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningFloatEvent(string eventName, UnityAction<float> lisnter)
        {
            Init();
            FloatEvent thisEvent = null;
            if (mFloatEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new FloatEvent();
                thisEvent.AddListener(lisnter);
                mFloatEventDictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningGameObjectEvent(string eventName, UnityAction<GameObject> lisnter)
        {
            Init();
            GameObjectEvent thisEvent = null;
            if (mGameobjectEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new GameObjectEvent();
                thisEvent.AddListener(lisnter);
                mGameobjectEventDictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningTransformEvent(string eventName, UnityAction<Transform> lisnter)
        {
            Init();
            TransformEvent thisEvent = null;
            if (mTransformEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new TransformEvent();
                thisEvent.AddListener(lisnter);
                mTransformEventDictionary.Add(eventName, thisEvent);
            }
        }
        public static void StartListeningTakeDamageEvent(string eventName, UnityAction<GameObject,int> lisnter)
        {
            Init();
            TakeDamageEvent thisEvent = null;
            if (mTakeDamageEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(lisnter);
            }
            else
            {
                thisEvent = new TakeDamageEvent();
                thisEvent.AddListener(lisnter);
                mTakeDamageEventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void TriggerCommonEvent(string eventName)
        {
            Init();
            CommonEvent thisEvent = null;
            if (mEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
        public static void TriggerStringEvent(string eventName, string value)
        {
            Init();
            StringEvent thisEvent = null;
            if (mStringEventictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(value);
            }
        }
        public static void TriggerIntEvent(string eventName, int value)
        {
            Init();
            IntEvent thisEvent = null;
            if (mIntEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(value);
            }
        }
        public static void TriggerFloatEvent(string eventName, float value)
        {
            Init();
            FloatEvent thisEvent = null;
            if (mFloatEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(value);
            }
        }
        public static void TriggerGameObjectEvent(string eventName, GameObject value)
        {
            Init();
            GameObjectEvent thisEvent = null;
            if (mGameobjectEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(value);
            }
        }
        public static void TriggerTransformEvent(string eventName, Transform value)
        {
            Init();
            TransformEvent thisEvent = null;
            if (mTransformEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(value);
            }
        }
        public static void TriggerTakeDamageEvent(string eventName, GameObject target, int damage)
        {
            Init();
            TakeDamageEvent thisEvent = null;
            if (mTakeDamageEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(target, damage);
            }
        }

        public static void StopListeningCommonEvent(string eventName, UnityAction listener)
        {
            Init();
            CommonEvent thisEvent = null;
            if (mEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningStringEvent(string eventName, UnityAction<string> listener)
        {
            Init();
            StringEvent thisStringEvent = null;
            if (mStringEventictionary.TryGetValue(eventName, out thisStringEvent))
            {
                thisStringEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningIntEvent(string eventName, UnityAction<int> listener)
        {
            Init();
            IntEvent thisEvent = null;
            if (mIntEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningFloatEvent(string eventName, UnityAction<float> listener)
        {
            Init();
            FloatEvent thisEvent = null;
            if (mFloatEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningGameObjectEvent(string eventName, UnityAction<GameObject> listener)
        {
            Init();
            GameObjectEvent thisEvent = null;
            if (mGameobjectEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningTransformEvent(string eventName, UnityAction<Transform> listener)
        {
            Init();
            TransformEvent thisEvent = null;
            if (mTransformEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
        public static void StopListeningTakeDamageEvent(string eventName, UnityAction<GameObject,int> listener)
        {
            Init();
            TakeDamageEvent thisEvent = null;
            if (mTakeDamageEventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }
    }
}