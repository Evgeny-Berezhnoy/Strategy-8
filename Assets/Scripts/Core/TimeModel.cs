using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TimeModel : ITimeModel, ITickable
{

    #region Fields

    private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

    #endregion

    #region Interfaces properties

    public IObservable<int> GameTime => _gameTime.Select(f => (int)f);

    #endregion

    #region Interfaces methods

    public void Tick()
    {
        _gameTime.Value += Time.deltaTime; 
    }

    #endregion

}