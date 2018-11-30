using System;
using Arbor;
using UniRx;
using UnityEngine;
using ExtendedAssets.UniRx;


namespace ExtendedAssets.Arbor
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Transision/ConditionalTransition")]
    public class ConditionalTransition : StateBehaviour
    {
        [SerializeField] private ObservableBoolTrigger trigger;
        [SerializeField] private StateLink trueStateLink;
        [SerializeField] private StateLink falseStateLink;

        private IDisposable observer;

        public override void OnStateBegin()
        {
            observer = trigger.AsObservable()
                .Subscribe(Transition);
        }

        public override void OnStateEnd()
        {
            observer.Dispose();
        }

        private void Transition(bool cond)
        {
            if (cond)
            {
                Transition(trueStateLink);
            }
            else
            {
                if (falseStateLink == null) return;

                Transition(falseStateLink);
            }
        }
    }
}
