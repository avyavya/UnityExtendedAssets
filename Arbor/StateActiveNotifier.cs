using Arbor;
using UnityEngine;


namespace ExtendedAssets.Arbor
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Notify/StateChangeNotifier")]
    public class StateActiveNotifier : StateBehaviour
    {
        [SerializeField] private ObservableBoolValueTrigger observer;

        public override void OnStateBegin()
        {
            observer.Value = true;
        }

        public override void OnStateEnd()
        {
            observer.Value = false;
        }
    }
}
