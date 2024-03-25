using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);

        void StopCoroutine(Coroutine routine);
    }
}