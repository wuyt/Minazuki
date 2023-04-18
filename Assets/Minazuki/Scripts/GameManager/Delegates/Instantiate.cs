using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minazuki
{
    /// <summary>
    /// 实例化
    /// </summary>
    public class Instantiate
    {
        public delegate UniTask<Transform> InstantiateDelegate(InstantiateModel model);
        public delegate UniTask InstantiateListDelegate(List<InstantiateModel> models);
        public InstantiateDelegate OnInstantion;
        public InstantiateListDelegate OnInstantionList;
        /// <summary>
        /// 根据实例化模型实例化预制件
        /// </summary>
        /// <param name="model">实例化模型</param>
        /// <returns>实例化后的预制件</returns>
        public async UniTask<Transform> Instantiation(InstantiateModel model)
        {
            if (OnInstantion == null)
            {
                Debug.LogError("OnInstantion is null");
                return null;
            }
            else
            {
                return await OnInstantion(model);
            }
        }

        public async UniTask Instantiation(List<InstantiateModel> models)
        {
            if (OnInstantionList == null)
            {
                Debug.LogError("OnInstantionList is null");
            }
            else
            {
                await OnInstantionList(models);
            }
        }
    }
}

