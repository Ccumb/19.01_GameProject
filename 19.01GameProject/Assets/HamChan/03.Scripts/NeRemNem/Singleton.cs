using UnityEngine;

namespace Neremnem.Tools
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T mInstance;        
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = FindObjectOfType<T>();
                    if (mInstance == null)
                    {
                        GameObject obj = new GameObject();
                        mInstance = obj.AddComponent<T>();
                    }
                }
                return mInstance;
            }
        }
        protected virtual void Awake()
        {
            mInstance = this as T;
        }
    }
}
