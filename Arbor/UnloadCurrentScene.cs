using UnityEngine;
using UnityEngine.SceneManagement;


namespace Arbor.StateMachine.StateBehaviours
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
            SceneManager.UnloadSceneAsync(gameObject.scene.name);
#else
			SceneManager.UnloadScene(gameObject.scene.name);
#endif
        }
    }
}
