using UnityEngine;

public class UnitStopCommand : CommandExecutorBase<IStopCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IStopCommand command)
    {

        Debug.Log($"Unit {name} has stopped.");

    }

    #endregion

}