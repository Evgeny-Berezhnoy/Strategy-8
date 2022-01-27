using System;
using UnityEngine;

public interface IReactiveSubscriber<T> : IObservable<T>
{

    #region Methods

    void Subscribe(Action<T> notification);
    
    void Subscribe(GameObject visitor, Action<T> notification);

    void Notify();

    #endregion

}