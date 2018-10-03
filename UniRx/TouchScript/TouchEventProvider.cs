using System;
using TouchScript.Gestures;
using UniRx;
using UnityEngine;


namespace ExtendedAssets.UniRx.TouchScript
{
    public class TouchEventProvider : MonoBehaviour
    {
        private readonly Subject<Unit> tappedSubject = new Subject<Unit>();
        private readonly Subject<Unit> pressedSubject = new Subject<Unit>();
        private readonly Subject<Unit> longPressedSubject = new Subject<Unit>();
        private readonly Subject<Unit> releasedSubject = new Subject<Unit>();

        public IObservable<Unit> OnTapped => tappedSubject;

        public IObservable<Unit> OnPressed => pressedSubject;

        public IObservable<Unit> OnLongPressed => longPressedSubject;

        public IObservable<Unit> OnReleased => releasedSubject;

        private void Start()
        {
            BindTouchScriptEvent();
        }

        private void BindTouchScriptEvent()
        {
            var tapComponent = gameObject.GetComponent<TapGesture>();
            if (tapComponent)
            {
                tapComponent.TapAsObservable()
                    .Subscribe(_ => tappedSubject.OnNext(Unit.Default));
            }

            var pressComponent = gameObject.GetComponent<PressGesture>();
            if (pressComponent)
            {
                pressComponent.PressAsObservable()
                    .Subscribe(_ => pressedSubject.OnNext(Unit.Default));
            }

            var longPressComponent = gameObject.GetComponent<LongPressGesture>();
            if (longPressComponent)
            {
                longPressComponent.LongPressAsObservable()
                    .Subscribe(_ => longPressedSubject.OnNext(Unit.Default));
            }

            var releaseComponent = gameObject.GetComponent<ReleaseGesture>();
            if (releaseComponent)
            {
                releaseComponent.ReleaseAsObservable()
                    .Subscribe(_ => releasedSubject.OnNext(Unit.Default));
            }
        }
    }
}
