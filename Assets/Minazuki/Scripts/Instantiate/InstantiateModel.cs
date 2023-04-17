using System;
using UnityEngine;

namespace Minazuki
{
    [Serializable]
    public class InstantiateModel
    {
        public Transform prefab;
        public Transform gameObjectInScene;
        public string fullPath;

        public TargetType targetType;

        public string tag;
    }
}


