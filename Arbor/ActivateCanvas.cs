using System;
using Arbor;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// It will switch the enable canvas.
    /// Canvas でなくて GameObject でやったほうがいいかもなので
    /// その時は FSM 刺すオブジェクトを考える
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("GameObject/ActivateCanvas")]
    public sealed class ActivateCanvas : StateBehaviour
    {
        /// <summary>
        /// GameObject to switch the active.
        /// </summary>
        [SerializeField] private Canvas target;

        /// <summary>
        /// Active switching at the state start.
        /// </summary>
        [SerializeField] private bool beginState;

        /// <summary>
        /// Active switching at the state end.
        /// </summary>
        [SerializeField] private bool endState;

        // todo インターフェースにしたい感
        private LevelLifecycle loader;
        private IDisposable observer;

        public override void OnStateAwake()
        {
            loader = state.GetBehaviour<LevelLifecycle>();
        }

        public override void OnStateBegin()
        {
            if (!target) return;

            if (loader)
            {
                observer = loader.IsLoaded
                    .Skip(1)
                    .Subscribe(x =>
                    {
                        if (!target) return;

                        target.enabled = x ? beginState : endState;
                    });
            }
            else
            {
                target.enabled = beginState;
            }
        }

        public override void OnStateEnd()
        {
            if (!target) return;

            if (loader)
            {
                observer?.Dispose();
            }
            else
            {
                target.enabled = endState;
            }
        }
    }
}
