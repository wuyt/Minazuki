using System;
using UnityEngine;

namespace Minazuki
{
    public static class Common
    {
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
    }
}

