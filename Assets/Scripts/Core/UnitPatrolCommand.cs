using UnityEngine;

public class UnitPatrolCommand : CommandExecutorBase<IPatrolCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IPatrolCommand command)
    {

        Debug.Log("Patrol command has been executed.");

    }

    #endregion

}