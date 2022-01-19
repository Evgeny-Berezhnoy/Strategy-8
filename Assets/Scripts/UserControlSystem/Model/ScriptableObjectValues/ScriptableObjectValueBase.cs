using System;
using UnityEngine;

public abstract class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
{

    #region Fields

    public Action<T> OnNewValue;

    #endregion

    #region Properties

    public T CurrentValue { get; private set; }

    #endregion

    #region Interfaces Methods

    public IAwaiter<T> GetAwaiter()
    {
        
        return new NewValueNotifier<ScriptableObjectValueBase<T>, T>(this);

    }

    #endregion

    #region Methods

    public void SetValue(T value)
    {

        CurrentValue = value;

        OnNewValue?.Invoke(value);

    }

    #endregion

    #region Nested classes

    public class NewValueNotifier<TBase, TResult> : AwaiterBase<TBase, TResult>
        where TBase : ScriptableObjectValueBase<TResult>
    {

        #region Fields

        private TResult _result;
        
        #endregion

        #region Constructors

        public NewValueNotifier(TBase baseObject) : base(baseObject)
        {
            _baseObject.OnNewValue += OnBreak;
        }

        #endregion

        #region Base Methods

        public override TResult GetResult()
        {
            return _result;
        }

        #endregion

        #region Methods

        protected void OnBreak(TResult obj)
        {
            _result                 = obj;
            _baseObject.OnNewValue -= OnBreak;
            
            OnBreak();
        }

        #endregion

    }

    #endregion

}