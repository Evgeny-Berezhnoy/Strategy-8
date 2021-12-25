using UnityEngine;

public class UnitStopCommand : CommandExecutorBase<IStopCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IStopCommand command)
    {

        Debug.Log("Stop command has been executed.");

    }

    #endregion

}