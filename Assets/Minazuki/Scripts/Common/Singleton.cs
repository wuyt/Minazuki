using UnityEngine;

namespace Minazuki
{
    /// <summary>
    /// 单实例
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// 实例
        /// </summary>
        private static T _instance;
        /// <summary>
        /// 实例
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
        }
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) { _instance = null; }
        }
    }
}

