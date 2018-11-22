using System;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.UniRx
{
    public class UnitObservableTrigger : MonoBehaviour
    {
        private Subject<Unit> subject;

        public IObservable<Unit> AsObservable()
        {
            return subject ?? (subject = new Subject<Unit>());
        }

        public void OnNext()
        {
            subject?.OnNext(Unit.Default);
        }
    }
}
