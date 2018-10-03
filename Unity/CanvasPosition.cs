using UnityEngine;


namespace ExtendedAssets.Unity
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasPosition : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Canvas canvas;
        private RectTransform rect;

        private void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(mainCamera);
            UnityEngine.Assertions.Assert.IsNotNull(canvas);

            rect = canvas.GetComponent<RectTransform>();
        }

        public Vector2 GetCanvasPositionFromWorldSpace(Vector3 world)
        {
            return canvas.renderMode == RenderMode.ScreenSpaceOverlay ? GetScreenSpaceOverlayPosition(world) : GetCanvasPosition(world);
        }

        private Vector2 GetScreenSpaceOverlayPosition(Vector3 position)
        {
            var pos = RectTransformUtility.WorldToScreenPoint(mainCamera, position);
            return pos;
        }

        private Vector2 GetCanvasPosition(Vector3 position)
        {
            var point = RectTransformUtility.WorldToScreenPoint(mainCamera, position);

            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, point, canvas.worldCamera, out pos);

            return pos;
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void Reset()
        {
            mainCamera = Camera.main;

            var cv = GetComponent<Canvas>();

            if (cv)
            {
                canvas = cv;
            }
            else
            {
                foreach (var go in gameObject.scene.GetRootGameObjects())
                {
                    cv = go.GetComponent<Canvas>();

                    if (!cv) continue;

                    canvas = cv;
                    break;
                }
            }

        }
    }
}
