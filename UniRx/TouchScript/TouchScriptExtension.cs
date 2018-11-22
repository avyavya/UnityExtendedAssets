using System;
using TouchScript.Gestures;
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
    }
}
