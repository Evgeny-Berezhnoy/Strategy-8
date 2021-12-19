using UnityEngine;

public class UnitMoveCommand : CommandExecutorBase<IMoveCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IMoveCommand command)
    {

        Debug.Log("Move command has been executed.");

    }

    #endregion

}