using System;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.Arbor
{
    public sealed class ObservableBoolValueTrigger : MonoBehaviour
    {
        private readonly Subject<bool> boolSubject = new Subject<bool>();

        public bool Value
        {
            set { boolSubject.OnNext(value); }
        }

        public IObservable<bool> OnSetValueAsObservable()
        {
            return boolSubject;
        }

        private void OnDestroy()
        {
            boolSubject.OnCompleted();
        }
    }
}
