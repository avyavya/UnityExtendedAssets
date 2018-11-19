using System;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.UniRx
{
    public class AnimatorStateExitTrigger : StateMachineBehaviour
    {
        /// Animator.StringToHash
        private readonly Subject<int> subject = new Subject<int>();

        public IObservable<int> AsObservable()
        {
            return subject.AsObservable();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            subject.OnNext(stateInfo.shortNameHash);
            subject.OnCompleted();
        }
    }
}
