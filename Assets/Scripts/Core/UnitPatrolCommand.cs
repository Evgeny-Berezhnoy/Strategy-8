using UnityEngine;

public class UnitPatrolCommand : CommandExecutorBase<IPatrolCommand>
{

    #region Base Methods

    public override void ExecuteSpecificCommand(IPatrolCommand command)
    {

        Debug.Log($"Unit {name} is patroling area near {command.Target} coordinates.");

    }

    #endregion

}