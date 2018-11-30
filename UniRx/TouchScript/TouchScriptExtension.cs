using System;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UniRx;


namespace ExtendedAssets.UniRx.TouchScript
{
    public static class TouchScriptExtension
    {
        public static IObservable<Unit> TapAsObservable(this TapGesture gesture)
        {
            return Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.Tapped += h,
                    h => gesture.Tapped -= h)
                .TakeUntilDestroy(gesture)
                .AsUnitObservable();
        }

        public static IObservable<Unit> PressAsObservable(this PressGesture gesture)
        {
            return Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.Pressed += h,
                    h => gesture.Pressed -= h)
                .AsUnitObservable();
        }

        public static IObservable<Unit> LongPressAsObservable(this LongPressGesture gesture)
        {
            return Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.LongPressed += h,
                    h => gesture.LongPressed -= h)
                .AsUnitObservable();
        }

        public static IObservable<Unit> ReleaseAsObservable(this ReleaseGesture gesture)
        {
            return Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.Released += h,
                    h => gesture.Released -= h)
                .AsUnitObservable();
        }

        public static IObservable<TransformGesture> OnTransformStartedAsObservable(this TransformGesture gesture)
        {
            var o = Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.TransformStarted += h,
                    h => gesture.TransformStarted -= h)
                .TakeUntilDestroy(gesture)
                .Select(x => gesture);

            return o;
        }

        public static IObservable<TransformGesture> OnTransformedAsObservable(this TransformGesture gesture)
        {
            return Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.Transformed += h,
                    h => gesture.Transformed -= h)
                .TakeUntilDestroy(gesture)
                .Select(x => gesture);
        }

        public static IObservable<TransformGesture> OnTransformCompleteAsObservable(this TransformGesture gesture)
        {
            var o = Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => h.Invoke,
                    h => gesture.TransformCompleted += h,
                    h => gesture.TransformCompleted -= h)
                .TakeUntilDestroy(gesture)
                .Select(x => gesture);

            return o;
        }
    }
}
