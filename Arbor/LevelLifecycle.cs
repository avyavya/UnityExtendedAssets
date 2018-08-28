using UnityEngine;
using UnityEngine.SceneManagement;
using Arbor;
using UniRx;
using Debug = UnityEngine.Debug;


namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// Load the specified scene.
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("Scene/LevelLifecycle")]
    [BuiltInBehaviour]
    public sealed class LevelLifecycle : StateBehaviour
    {
        /// <summary>
        /// The name of the load scene.
        /// </summary>
        [SerializeField] private string levelName;

        /// <summary>
        /// Load scene mode
        /// </summary>
        [SerializeField] private LoadSceneMode loadSceneMode = LoadSceneMode.Additive;

        /// <summary>
        /// Activate the loaded scene.(LoadSceneMode.Additive only)
        /// </summary>
        [SerializeField] private FlexibleBool isActiveScene = new FlexibleBool(false);

        /// <summary>
        /// Transition at done of scene loading(LoadSceneMode.Additive only)
        /// </summary>
        [SerializeField] private StateLink loaded = new StateLink();

        [SerializeField] private StateLink unloaded = new StateLink();

        private string ScenePath { get; set; }

        // Use this for enter state
        public override void OnStateBegin()
        {
#if UNITY_EDITOR
            if (IsSceneExists(levelName)) return;
#endif
            SceneManager.LoadSceneAsync(levelName, loadSceneMode)
                .AsAsyncOperationObservable()
                .Where(_ => _.isDone)
                .Select(_ => SceneManager.GetSceneByName(levelName))
                .Where(_ => _.isLoaded)
                .Subscribe(OnLoaded);
        }

        private void OnLoaded(Scene scene)
        {
            Debug.Log("Scene " + scene.name + " is loaded.");
            ScenePath = scene.path;

            if (loadSceneMode != LoadSceneMode.Additive || !scene.IsValid()) return;

            if (isActiveScene.value)
            {
                SceneManager.SetActiveScene(scene);
            }

            Debug.Log("Register delegate. " + scene.name);
            SceneManager.sceneUnloaded += OnUnloaded;
            Transition(loaded);
        }

        private void OnUnloaded(Scene scene)
        {
            if (scene.path != ScenePath) return;

            Debug.Log("Scene " + scene.name + " is unloaded. " + scene.path);
            SceneManager.sceneUnloaded -= OnUnloaded;
            Transition(unloaded);
        }

#if UNITY_EDITOR
        private static bool IsSceneExists(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);

            return scene.isLoaded;
        }
#endif
    }
}
