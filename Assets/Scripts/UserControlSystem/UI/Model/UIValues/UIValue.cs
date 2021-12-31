using System;
using UnityEngine;

public abstract class UIValue<T> : ScriptableObject
{

    #region Fields

    public Action<T> OnNewValue;

    #endregion

    #region Properties

    public T CurrentValue { get; private set; }

    #endregion

    #region Methods

    public void SetValue(T value)
    {

        CurrentValue = value;

        OnNewValue?.Invoke(value);

    }

    #endregion

}