using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Neremnem.AI
{
    public class BlackBoard
    {
        private static Dictionary<string, bool> mBoolKey = new Dictionary<string, bool>();
        private static Dictionary<string, GameObject> mGameObjectKey = new Dictionary<string, GameObject>();
        private static Dictionary<string, float> mFloatKey = new Dictionary<string, float>();
        private static Dictionary<string, string> mStringKey = new Dictionary<string, string>();
        private static Dictionary<string, int> mIntKey = new Dictionary<string, int>();
        private static Dictionary<string, Vector3> mVector3Key = new Dictionary<string, Vector3>();
        private static Dictionary<string, Transform> mTrnasformKey = new Dictionary<string, Transform>();

        public static void AddBoolKey(string key, bool value)
        {
            if (mBoolKey == null)
            {
                mBoolKey = new Dictionary<string, bool>();
            }
            mBoolKey.Add(key, value);
        }
        public static void AddGameObjectKey(string key, GameObject value)
        {
            if (mGameObjectKey == null)
            {
                mGameObjectKey = new Dictionary<string, GameObject>();
            }
            mGameObjectKey.Add(key, value);
        }
        public static void AddFloatKey(string key, float value)
        {
            if (mFloatKey == null)
            {
                mFloatKey = new Dictionary<string, float>();
            }
            mFloatKey.Add(key, value);
        }
        public static void AddStringKey(string key, string value)
        {
            if (mStringKey == null)
            {
                mStringKey = new Dictionary<string, string>();
            }
            mStringKey.Add(key, value);
        }
        public static void AddIntKey(string key, int value)
        {
            if (mIntKey == null)
            {
                mIntKey = new Dictionary<string, int>();
            }
            mIntKey.Add(key, value);
        }
        public static void AddVector3Key(string key, Vector3 value)
        {
            if (mVector3Key == null)
            {
                mVector3Key = new Dictionary<string, Vector3>();
            }
            mVector3Key.Add(key, value);
        }
        public static void AddTransformKey(string key, Transform value)
        {
            if (mTrnasformKey == null)
            {
                mTrnasformKey = new Dictionary<string, Transform>();
            }
            mTrnasformKey.Add(key, value);
        }


        public static bool GetValueByBoolKey(string key)
        {
            bool value;
            mBoolKey.TryGetValue(key, out value);
            return value;
        }
        public static GameObject GetValueByGameObjectKey(string key)
        {
            GameObject value;
            mGameObjectKey.TryGetValue(key, out value);
            return value;
        }
        public static float GetValueByFloatKey(string key)
        {
            float value;
            mFloatKey.TryGetValue(key, out value);
            return value;
        }
        public static string GetValueByStringKey(string key)
        {
            string value;
            mStringKey.TryGetValue(key, out value);
            return value;
        }
        public static int GetValueByIntKey(string key)
        {
            int value;
            mIntKey.TryGetValue(key, out value);
            return value;
        }
        public static Vector3 GetValueByVector3Key(string key)
        {
            Vector3 value;
            mVector3Key.TryGetValue(key, out value);
            return value;
        }
        public static Transform GetValueByTransformKey(string key)
        {
            Transform value;
            mTrnasformKey.TryGetValue(key, out value);
            return value;
        }

        public static void SetValueByBoolKey(string key, bool value)
        {
            mBoolKey[key] = value;
        }
        public static void SetValueByGameObjectKey(string key, GameObject value)
        {
            mGameObjectKey[key] = value;
        }
        public static void SetValueByFloatKey(string key, float value)
        {
            mFloatKey[key] = value;
        }
        public static void SetValueByStringKey(string key, string value)
        {
            mStringKey[key] = value;
        }
        public static void SetValueByIntKey(string key, int value)
        {
            mIntKey[key] = value;
        }
        public static void SetValueByVector3Key(string key, Vector3 value)
        {
            mVector3Key[key] = value;
        }
        public static void SetValueByTransformKey(string key, Transform value)
        {
            mTrnasformKey[key] = value;
        }

        public static void DeleteStringKey(string key)
        {
            mStringKey.Remove(key);
        }
        public static void DeleteIntKey(string key)
        {
            mIntKey.Remove(key);
        }
    }
}
