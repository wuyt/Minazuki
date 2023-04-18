using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Minazuki
{
    /// <summary>
    /// 共用类
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 编辑器行间距
        /// </summary>
        public const int lineSpacing = 2;

        public const string mColor = "#16A085";

        /// <summary>
        /// 根据（场景中）游戏对象获取完整路径
        /// </summary>
        /// <param name="value">游戏对象</param>
        /// <returns>完整路径</returns>
        public static string GetFullPathByGameObject(Transform value)
        {
            Transform tf = value;
            string path = tf.name;
            while (tf.parent != null)
            {
                tf = tf.parent;
                path = string.Format("{0}/{1}", tf.name, path);
            }
            path = String.Format("/{0}", path);
            return path;
        }
        /// <summary>
        /// 根据完整路径返回最末尾游戏对象名称
        /// </summary>
        /// <param name="value">完整路径</param>
        /// <returns>游戏对象名称</returns>
        public static string GetLastGameObjectNameByFullPath(string value)
        {
            string[] path = value.Split('/');
            return path[path.Length - 1];
        }

        public static bool TryGetTransformByFullPath(string fullPath,out Transform value)
        {
            value = null;
            if (fullPath == null)
            {
                return false;
            }
            if (fullPath.Trim().Length == 0)
            {
                return false;
            }

            string path = fullPath;
            if (fullPath.Substring(0, 1).Equals("/"))
            {
                path = fullPath.Remove(0, 1);
            }

            if (path.Length == 0)
            {
                return false;
            }

            var array = path.Split('/');

            //用这个方法是为了获取场景中被禁用掉的根游戏对象
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            if (roots.Where(x => x.name.Equals(array[0])).Count() == 0)
            {
                //为了获取DontDestroyOnLoad从其他场景过来的游戏对象
                value = GameObject.Find(string.Format("/{0}", array[0])).transform;
                if (value == null) return false;
            }
            else
            {
                value = roots.Where(x => x.name.Equals(array[0])).First().transform;
            }

            for (int i = 1; i < array.Length; i++)
            {
                value = value.Find(array[i]);
                if (value == null)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
