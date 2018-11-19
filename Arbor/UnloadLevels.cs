using Arbor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ExtendedAssets.Arbor
{
    /// <inheritdoc />
    /// <summary>
    /// 指定したシーン名でロードされていたらアンロードする
    /// UnloadLevel はロードされてないシーン名だと例外
    /// </summary>
    [AddComponentMenu("")]
    [AddBehaviourMenu("Scene/UnloadLevels")]
    public sealed class UnloadLevels : StateBehaviour
    {
        /// <summary>
        /// The name of the scene to be unloaded.
        /// </summary>
        [SerializeField] private string[] levelNames = { };

        [SerializeField] private StateLink next = new StateLink();

        // Use this for enter state
        public override void OnStateBegin()
        {
            var scenes = new List<Scene>(SceneManager.sceneCount);

            for (var i = SceneManager.sceneCount - 1; i >= 0; --i)
            {
                scenes.Add(SceneManager.GetSceneAt(i));
            }

            foreach (var levelName in levelNames)
            {
                foreach (var scene in scenes)
                {
                    if (scene.name != levelName) continue;
#if UNITY_5_5_OR_NEWER
                    SceneManager.UnloadSceneAsync(scene);
#else
                    SceneManager.UnloadScene(scene);
#endif
                    scenes.Remove(scene);
                    break;
                }
            }

            Transition(next);
        }
    }
}
