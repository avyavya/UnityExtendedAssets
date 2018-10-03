using System;
using UnityEngine;
using UniRx;


namespace ExtendedAssets.UniRx
{
    public class AnimatorStateEnterTrigger : StateMachineBehaviour
    {
        /// Animator.StringToHash
        private readonly Subject<int> subject = new Subject<int>();

        public IObservable<int> AsObservable()
        {
            return subject.AsObservable();
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            subject.OnNext(stateInfo.shortNameHash);
        }
    }
}
