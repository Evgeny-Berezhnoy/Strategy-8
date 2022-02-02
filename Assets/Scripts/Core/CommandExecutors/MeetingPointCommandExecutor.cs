using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(ProduceUnitCommandExecutor))]
public class MeetingPointCommandExecutor : CommandExecutorBase<IRendezvousPointCommand>
{
    #region Fields

    [SerializeField] protected Transform _pivotPoint;

    #endregion

    #region Properties

    public Transform PivotPoint => _pivotPoint;

    #endregion

    #region Base methods

    public async override Task ExecuteSpecificCommand(IRendezvousPointCommand command)
    {
        _pivotPoint.position = command.Target;
    }

    #endregion
}