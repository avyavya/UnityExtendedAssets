using Arbor;
using UnityEngine;
using UniRx;
using ExtendedAssets.UniRx;


namespace ExtendedAssets.Arbor
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Transision/UnitObservableTransition")]
    public sealed class ObservableUnitTransition : StateBehaviour
    {
        [SerializeField] private ObservableUnitTrigger trigger;
        [SerializeField] private StateLink stateLink = new StateLink();

        public override void OnStateBegin()
        {
            trigger.AsObservable()
                .Subscribe(_ => Transition(stateLink));
        }

        public override void OnStateEnd()
        {
        }
    }
}
