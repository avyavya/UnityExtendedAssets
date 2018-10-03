using UniRx;
using UnityEngine;
using System;


namespace ExtendedAssets.UniRx
{
    public class ResourceLoader
    {
        private readonly string path;

        private readonly Progress<float> progress = new Progress<float>();

        public float Progress { get; private set; }

        public ResourceLoader(string path)
        {
            this.path = path;
            progress.ProgressChanged += OnProgress;
        }

        private void OnProgress(object obj, float p)
        {
            Progress = p;
        }

        public IObservable<UnityEngine.Object> AsObservable()
        {
            var o = Observable.Create<UnityEngine.Object>(observer =>
            {
                var req = Resources.LoadAsync(path);

                return req.AsAsyncOperationObservable(progress)
                    .Where(x => x.isDone)
                    .Subscribe(_ => OnResourceLoaded(observer, _.asset));
            });

            return o;
        }

        private void OnResourceLoaded(IObserver<UnityEngine.Object> observer, UnityEngine.Object obj)
        {
            observer.OnNext(obj);
            observer.OnCompleted();

            progress.ProgressChanged -= OnProgress;
        }
    }
}
