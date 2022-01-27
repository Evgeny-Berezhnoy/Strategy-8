using System;
using UnityEngine;

public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>, IReactiveSubscriber<T>, IDisposableAdvanced
{

    #region Interfaces Properties

    public bool IsDisposed { get; private set; } 

    #endregion

    #region Properties

    public T CurrentValue { get; private set; }

    #endregion

    #region Interfaces Methods

    public abstract IAwaiter<T> GetAwaiter();

    public abstract IDisposable Subscribe(IObserver<T> observer);

    public abstract void Subscribe(Action<T> notification);

    public abstract void Subscribe(Predicate<bool> subscribtionRetentionPredicate, Action<T> notification);
    
    public abstract void Subscribe(GameObject visitor, Action<T> notification);

    public abstract void Notify();

    public virtual void Dispose()
    {
        if (IsDisposed) return;

        IsDisposed = true;

        GC.SuppressFinalize(this);
    }

    #endregion

    #region Methods

    public virtual void SetValue(T value)
    {
        CurrentValue = value;
    }

    #endregion

    #region Nested classes

    // Example
    public class NewValueNotifier<TBase, TResult> : AwaiterBase<TBase, TResult>
        where TBase : ScriptableObjectValueBase<TResult>
    {

        #region Fields

        private TResult _result;

        #endregion

        #region Constructors

        public NewValueNotifier(TBase baseObject) : base(baseObject)
        {
            _baseObject.Subscribe(_ => !IsCompleted, value => OnNewValue(value));
        }

        #endregion

        #region Base Methods

        public override TResult GetResult()
        {
            return _result;
        }

        #endregion

        #region Methods

        protected void OnNewValue(TResult obj)
        {
            _result = obj;
            
            OnBreak();
        }

        #endregion

    }

    #endregion

}