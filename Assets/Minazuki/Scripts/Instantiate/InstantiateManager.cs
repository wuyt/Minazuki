using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.Linq;
using UnityEngine;

namespace Minazuki
{
    /// <summary>
    /// 实例化管理器
    /// </summary>
    public class InstantiateManager : Singleton<InstantiateManager>
    {
        /// <summary>
        /// 是否开始控制台显示
        /// </summary>
        public bool isDebug = true;
        /// <summary>
        /// 完成实例化的对象
        /// </summary>
        Transform instantiated;
        /// <summary>
        /// 参照对象
        /// </summary>
        Transform reference;
        /// <summary>
        /// 父节点对象
        /// </summary>
        Transform parent;

        protected override void Awake()
        {
            base.Awake();
            GameManager.Instance.Instantiate.OnInstantion += Instantiate;
            GameManager.Instance.Instantiate.OnInstantionList += InstantiateList;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (GameManager.Instance) GameManager.Instance.Instantiate.OnInstantion -= Instantiate;
            if (GameManager.Instance) GameManager.Instance.Instantiate.OnInstantionList -= InstantiateList;
        }
        /// <summary>
        /// 实例化列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <returns></returns>
        private async UniTask InstantiateList(List<InstantiateModel> models)
        {
            if (models == null)
            {
                Debug.LogError("When instantiated, the models is null.");
            }

            foreach (var item in models)
            {
                await Instantiate(item);
            }
        }
        /// <summary>
        /// 实例化模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>完成实例化的对象</returns>
        private async UniTask<Transform> Instantiate(InstantiateModel model)
        {
            if (model == null)
            {
                Debug.LogError("When instantiated, the model is null.");
                return null;
            }

            if (model.prefab == null)
            {
                Debug.LogError("When instantiated, the Prefab is null.");
                return null;
            }

            if (model.setParent) Common.TryGetTransformByFullPath(model.parentPath, out parent);
            if (model.setReference) Common.TryGetTransformByFullPath(model.referencePath, out reference);

            if (model.setParent)
            {
                switch (model.targetType)
                {
                    case TargetType.Self:
                        if (model.setReference)
                        {
                            await InstantiateIEnumerator(model.prefab, parent, reference.position, reference.rotation).ToUniTask(this);
                            return instantiated;
                        }
                        else
                        {
                            await InstantiateIEnumerator(model.prefab, parent).ToUniTask(this);
                            return instantiated;
                        }
                    case TargetType.Children:
                        parent.gameObject.Children().ForEach(async x =>
                        {
                            await InstantiateIEnumerator(model.prefab, x.transform).ToUniTask(this);
                        });
                        return null;
                    case TargetType.Tag:
                        parent.gameObject.Descendants().ForEach(async x =>
                        {
                            if (x.CompareTag(model.tag))
                            {
                                await InstantiateIEnumerator(model.prefab, x.transform).ToUniTask(this);
                            }
                        });
                        return null;
                }
            }
            else
            {
                if (model.setReference)
                {
                    await InstantiateIEnumerator(model.prefab, reference.position, reference.rotation).ToUniTask(this);
                    return instantiated;
                }
                else
                {
                    await InstatiateIEnumerator(model.prefab).ToUniTask(this);
                    instantiated = Instantiate(model.prefab);
                    return instantiated;
                }
            }
            return null;
        }
        /// <summary>
        /// 实例化协程
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <returns></returns>
        private IEnumerator InstatiateIEnumerator(Transform prefab)
        {
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:start instantiate prefab   {1} :{2}", Common.mColor, prefab.name, Time.time));
            yield return instantiated = Instantiate(prefab);
            instantiated.name = prefab.name;
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:end instantiate prefab   {1} :{2}", Common.mColor, prefab.name, Time.time));
        }
        /// <summary>
        /// 实例化协程
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <param name="parent">父节点</param>
        /// <returns></returns>
        private IEnumerator InstantiateIEnumerator(Transform prefab, Transform parent)
        {
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:start instantiate prefab   {1} => {2} :{3}", Common.mColor, prefab.name, parent.name, Time.time));
            yield return instantiated = Instantiate(prefab, parent);
            instantiated.name = prefab.name;
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:end instantiate prefab   {1} => {2} :{3}", Common.mColor, prefab.name, parent.name, Time.time));
        }
        /// <summary>
        /// 实例化协程
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">角度</param>
        /// <returns></returns>
        private IEnumerator InstantiateIEnumerator(Transform prefab, Vector3 position, Quaternion rotation)
        {
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:start instantiate prefab   {1} => {2} :{3}", Common.mColor, prefab.name, reference.name, Time.time));
            yield return instantiated = Instantiate(prefab, position, rotation);
            instantiated.name = prefab.name;
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:end instantiate prefab   {1} => {2} :{3}", Common.mColor, prefab.name, reference.name, Time.time));
        }
        /// <summary>
        /// 实例化协程
        /// </summary>
        /// <param name="prefab">预制件</param>
        /// <param name="parent">父节点</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">角度</param>
        /// <returns></returns>
        private IEnumerator InstantiateIEnumerator(Transform prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:start instantiate prefab   {1} => {2}={3} :{4}", Common.mColor, prefab.name, parent.name, reference.name, Time.time));
            yield return instantiated = Instantiate(prefab, position, rotation, parent);
            instantiated.name = prefab.name;
            if (isDebug) Debug.Log(string.Format("<color={0}>InstantiateManager</color>:end instantiate prefab   {1} => {2}={3} :{4}", Common.mColor, prefab.name, parent.name, reference.name, Time.time));
        }
    }
}

