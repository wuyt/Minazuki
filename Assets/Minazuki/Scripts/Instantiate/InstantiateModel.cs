using System;
using UnityEngine;

namespace Minazuki
{
    /// <summary>
    /// 实例化模型
    /// </summary>
    [Serializable]
    public class InstantiateModel
    {
        /// <summary>
        /// 要实例化的预制件
        /// </summary>
        public Transform prefab;
        /// <summary>
        /// 是否设置父节点
        /// </summary>
        public bool setParent;
        /// <summary>
        /// 父节点游戏对象
        /// </summary>
        public Transform parentGameObject;
        /// <summary>
        /// 父节点完整路径
        /// </summary>
        public string parentPath;
        /// <summary>
        /// 目标类型
        /// </summary>
        public TargetType targetType;
        /// <summary>
        /// Tag标签
        /// </summary>
        public string tag;
        /// <summary>
        /// 是否设置参照游戏对象
        /// </summary>
        public bool setReference;
        /// <summary>
        /// 参照游戏对象
        /// </summary>
        public Transform referenceGameObject;
        /// <summary>
        /// 参照对象完整路径
        /// </summary>
        public string referencePath;

    }
}


