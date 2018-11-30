using System;
using Arbor;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.UniRx
{
    public class ObservableBoolTrigger : MonoBehaviour
    {
        private readonly Subject<bool> subject = new Subject<bool>();

        public IObservable<bool> AsObservable()
        {
            return subject;
        }

        public void OnNext(bool value)
        {
            subject.OnNext(value);
        }

        private void OnDestroy()
        {
            subject.OnCompleted();
        }
    }
}
