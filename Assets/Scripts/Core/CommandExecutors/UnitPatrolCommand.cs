using System.Threading.Tasks;
using UnityEngine;

public class UnitPatrolCommand : CommandExecutorBase<IPatrolCommand>
{

    #region Base Methods

    public async override Task ExecuteSpecificCommand(IPatrolCommand command)
    {

        Debug.Log($"Unit {name} is patroling area near {command.Target} coordinates.");

    }

    #endregion

}