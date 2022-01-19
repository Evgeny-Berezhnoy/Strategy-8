using System;

public abstract class AwaiterBase<TBase, TResult> : IAwaiter<TResult>
    where TBase : IAwaitable<TResult>
{

    #region Fields

    protected readonly TBase _baseObject;
    protected Action _continuation;
    
    #endregion

    #region Interfaces Properties

    public bool IsCompleted { get; protected set; }

    #endregion

    #region Constructors

    public AwaiterBase(TBase baseObject)
    {
        _baseObject = baseObject;
    }

    #endregion

    #region Interfaces Methods

    public virtual void OnCompleted(Action continuation)
    {
        if (IsCompleted)
        {
            continuation?.Invoke();
        }
        else
        {
            _continuation = continuation;
        };
    }

    protected virtual void OnBreak()
    {
        IsCompleted = true;

        _continuation?.Invoke();
    }

    public abstract TResult GetResult();
    
    #endregion

}