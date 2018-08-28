using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.EventSystems;


namespace ExtendedAssets.Unity
{
    /// <inheritdoc />
    /// <summary>
    /// UI のシーンがロードされた時に UI Camera を取ってくる
    /// Canvas に刺して使う
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UICameraFinder : MonoBehaviour
    {
        private void Start()
        {
            AttatchRenderCamera();
            SetUpEventSystem();
        }

        [ContextMenu("Attatch UI Camera")]
        private void AttatchRenderCamera()
        {
            var canvas = GetComponent<Canvas>();

#if UNITY_EDITOR
            if (!canvas)
            {
                Debug.LogError("Canvas not found");
                return;
            }
#endif

            if (canvas.worldCamera) return;

            for (var i = 0; i < SceneManager.sceneCount; ++i)
            {
                var scene = SceneManager.GetSceneAt(i);

                // シーンから UI Layer の Camera を探す
                var go = scene.GetRootGameObjects()
                    .FirstOrDefault(x => x.layer == LayerMask.NameToLayer("UI") && x.GetComponent<Camera>());

                if (!go)
                {
                    continue;
                }

                Debug.Log("UI Camera found in " + scene.name);
                var cam = go.GetComponent<Camera>();

                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = cam;
                return;
            }

            Debug.LogError("No UI Camera found.");
            SetUpUICamera();
        }

        /// UI Camera を作る. UI 製作時用
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void SetUpUICamera()
        {
            var go = new GameObject("UI Camera")
            {
                layer = LayerMask.NameToLayer("UI")
            };

            var cam = go.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.Depth;
            cam.cullingMask = 1 << LayerMask.NameToLayer("UI");
            cam.orthographic = true;
            cam.orthographicSize = 5;
            cam.farClipPlane = 100;
            cam.allowHDR = false;
            cam.allowMSAA = false;

            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = cam;
        }

        /// EventSystem がヒエラルキー上にあるかチェックして無ければ作る
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private static void SetUpEventSystem()
        {
            if (EventSystem.current) return;

            Debug.Log("EventSystem missing.");

            var go = new GameObject("EventSystem");
            var es = go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();

            EventSystem.current = es;
        }
    }
}
