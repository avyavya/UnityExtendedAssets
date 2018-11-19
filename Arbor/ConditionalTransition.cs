using Arbor;
using UniRx;
using UnityEngine;
using ExtendedAssets.UniRx;
using UnityEngine.Assertions;


namespace ExtendedAssets.Arbor
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Transision/ConditionalTransition")]
    public class ConditionalTransition : StateBehaviour
    {
        [SerializeField] private ObservableBoolTrigger trigger;
        [SerializeField] private StateLink trueStateLink;
        [SerializeField] private StateLink falseStateLink;

        public override void OnStateAwake()
        {
            Assert.IsNotNull(trigger);
            Assert.IsNotNull(trueStateLink);

            trigger.AsObservable()
                .Subscribe(Transition);
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
