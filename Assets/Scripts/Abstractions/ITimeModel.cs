using System;

public interface ITimeModel
{

    #region Properties

    IObservable<int> GameTime { get; }

    #endregion

}