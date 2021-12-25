using System;
using UnityEngine;
using Zenject;

public class UnitMoveCommand : CommandExecutorBase<IMoveCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IMoveCommand command)
    {

        Debug.Log($"{name} is moving to {command.Target}!");

    }

    #endregion

}