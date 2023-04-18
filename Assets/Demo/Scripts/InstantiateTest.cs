using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Minazuki.Demo
{
    public class InstantiateTest : MonoBehaviour
    {
        public InstantiateModel model;

        public List<InstantiateModel> models = new List<InstantiateModel>();
        void Start()
        {
            GameManager.Instance.Instantiate.Instantiation(model).Forget();

            GameManager.Instance.Instantiate.Instantiation(models).Forget();
        }
    }
}

