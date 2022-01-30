using System;
using System.Linq;
using UniRx;
using UnityEngine;

public abstract class StatefulSOValueBase<T> : ScriptableObjectValueBase<T>
{

    #region Fields

    protected ReactiveProperty<T> _valueNotifier = new ReactiveProperty<T>();

    #endregion

    #region Base methods

    public override IAwaiter<T> GetAwaiter()
    {
        return new NewValueNotifier<StatefulSOValueBase<T>, T>(this);
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
        _valueNotifier.Value = CurrentValue;
    }

    public override void SetValue(T value)
    {
        base.SetValue(value);

        Notify();
    }

    #endregion

}