using System;
using Arbor;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// It will transition by the toggle.
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("Transition/UI/UIToggle Coordinate GameObject")]
    public sealed class UIToggleCoordinateGameObjectBehaviour : StateBehaviour
    {
        /// <summary>
        /// Toggle to the judgment
        /// </summary>
        [SerializeField] private Toggle toggle;

        /// <summary>
        /// Coodinate state with Toggle value
        /// </summary>
        [SerializeField] private GameObject target;

        private IDisposable observer;

        // Use this for enter state
        public override void OnStateBegin()
        {
            if (!toggle || !target) return;

            observer = toggle.ObserveEveryValueChanged(x => x.isOn)
                .Subscribe(x => target.SetActive(x));
        }

        public override void OnStateEnd()
        {
            observer?.Dispose();
        }
    }
}
