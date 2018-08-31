using System.Linq;
using UnityEngine;
using Arbor;
using UniRx;
using UnityEngine.SceneManagement;


namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// Load the specified scene.
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("Scene/Load Multiple Levels as Additive")]
    public class LoadLevelsAdditive : StateBehaviour
    {
        /// <summary>
        /// Scene names for load
        /// </summary>
        [SerializeField] private string[] levelNames;

        /// <summary>
        /// Transition at done of all scene loading
        /// </summary>
        [SerializeField] private StateLink loaded = new StateLink();

        public override void OnStateBegin()
        {
            levelNames.Select(scene =>
            {
                return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive)
                    .AsAsyncOperationObservable()
                    .Where(_ => _.isDone)
                    .Select(_ => SceneManager.GetSceneByName(scene))
                    .Where(_ => _.isLoaded);
//                    .Do(_ => Debug.Log(_.name + " loaded."));
            }).WhenAll().Subscribe(_ => Transition(loaded));
        }
    }
}
