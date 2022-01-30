using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(ProduceUnitCommandExecutor))]
public class RendezvousPointCommandExecutor : CommandExecutorBase<IRendezvousPointCommand>
{

    #region Field

    private Transform _pivotPoint;

    #endregion

    #region Unity events

    private void Awake()
    {
        _pivotPoint = GetComponentInParent<ProduceUnitCommandExecutor>().PivotPoint;
    }

    #endregion

    #region Base methods

    public async override Task ExecuteSpecificCommand(IRendezvousPointCommand command)
    {
        _pivotPoint.position = command.Target;
    }

    #endregion

}