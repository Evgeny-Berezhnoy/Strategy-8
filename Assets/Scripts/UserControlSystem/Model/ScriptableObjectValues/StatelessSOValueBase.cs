using System;
using System.Linq;
using UnityEngine;
using UniRx;

public abstract class StatelessSOValueBase<T> : ScriptableObjectValueBase<T>
{

    #region Fields

    protected Subject<T> _valueNotifier = new Subject<T>();

    #endregion

    #region Base methods

    public override IAwaiter<T> GetAwaiter()
    {
        return new NewValueNotifier<StatelessSOValueBase<T>, T>(this);
    }

    public override IDisposable Subscribe(IObserver<T> observer)
    {
        return _valueNotifier.Subscribe(observer);
    }

    public override void Subscribe(Action<T> notification)
    {
        _valueNotifier
            .TakeWhile(_ => !IsDisposed)
            .Subscribe(value => notification(value));
    }

    public override void Subscribe(Predicate<bool> subscribtionRetentionPredicate, Action<T> notification)
    {
        _valueNotifier
            .TakeWhile(_ => subscribtionRetentionPredicate(false)) // false is not required parameter
            .Subscribe(value => notification(value));
    }

    public override void Subscribe(GameObject visitor, Action<T> notification)
    {
        _valueNotifier
            .TakeWhile(_ => !IsDisposed)
            .Subscribe(value => notification(value))
            .AddTo(visitor);
    }

    public override void Notify()
    {
        _valueNotifier.OnNext(CurrentValue);
    }

    #endregion
    
}