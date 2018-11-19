using System;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.UniRx
{
    public class ObservableBoolTrigger : MonoBehaviour
    {
        private Subject<bool> subject;

        public IObservable<bool> AsObservable()
        {
            if (subject == null)
            {
                subject = new Subject<bool>();
            }

            return subject.AsObservable();
        }

        public void OnNext(bool value)
        {
            subject?.OnNext(value);
        }

        private void OnDestroy()
        {
            subject?.OnCompleted();
        }
    }
}
