using System;
using Arbor;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


namespace ExtendedAssets.Arbor
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Transition/ObservablePointerClickTransition")]
    public class ObservablePointerClickTransition : StateBehaviour
    {
        [SerializeField] private ObservablePointerClickTrigger trigger;
        [SerializeField] private StateLink nextState = new StateLink();

        private IDisposable observer;

        public override void OnStateBegin()
        {
            observer = trigger.OnPointerClickAsObservable()
                .Subscribe(_ => OnClick());
        }

        public override void OnStateEnd()
        {
            observer.Dispose();
        }

        private void OnClick()
        {
            Transition(nextState);
        }
    }
}
