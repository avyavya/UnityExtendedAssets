using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// Unload the specified scene from the current scene.
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("Scene/Unload Current Scene")]
    public sealed class UnloadCurrentScene : StateBehaviour
    {
        // Use this for enter state
        public override void OnStateBegin()
        {
#if UNITY_5_5_OR_NEWER
            SceneManager.UnloadSceneAsync(gameObject.scene);
#else
			SceneManager.UnloadScene(gameObject.scene);
#endif
        }
    }
}
