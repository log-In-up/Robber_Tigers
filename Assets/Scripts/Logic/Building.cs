using UnityEngine;

namespace Assets.Scripts.Logic
{
    [DisallowMultipleComponent]
    public abstract class Building : MonoBehaviour
    {
        public delegate void VisitEventHandler();

        public event VisitEventHandler OnVisit;

        public virtual void OnVisitTiger()
        {
            OnVisit?.Invoke();
        }
    }
}