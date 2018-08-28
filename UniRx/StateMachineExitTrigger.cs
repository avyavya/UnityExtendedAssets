using System;
using UnityEngine;
using UniRx;


namespace ExtendedAssets.UniRx
{
    public sealed class StateMachineExitTrigger : StateMachineBehaviour
    {
        private readonly Subject<int> subject = new Subject<int>();

        public IObservable<int> AsObservable()
        {
            return subject.AsObservable();
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            subject.OnNext(stateMachinePathHash);
        }
    }
}
