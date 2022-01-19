﻿using UnityEngine;

public class UnitStopCommand : CommandExecutorBase<IStopCommand>, ICancellableTokenManager
{

    #region Interfaces Properties

    public CancellationTokenManager CancellationTokenManager { get; set; }

    #endregion

    #region Base Methods

    public override void ExecuteSpecificCommand(IStopCommand command)
    {
        CancellationTokenManager.CancelToken();

        Debug.Log($"Unit {name} has stopped.");
    }

    #endregion

}