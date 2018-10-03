using UnityEngine;


namespace ExtendedAssets.Unity
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;
        [HideInInspector]
        public new GameObject gameObject;

        /// テスト中
        /// 継承先で Awake 使う場合は base.Awake() してやらなくちゃいけないのがイマイチ
        protected virtual void Awake()
        {
            transform = base.transform;
            gameObject = base.gameObject;
        }
    }
}
