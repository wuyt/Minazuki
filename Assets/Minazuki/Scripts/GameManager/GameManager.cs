using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Minazuki
{
    public class GameManager : Singleton<GameManager>
    {
        public new Instantiate Instantiate = new Instantiate();

        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    }
}

