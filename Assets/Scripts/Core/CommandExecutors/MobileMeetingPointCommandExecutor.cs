using System.Threading.Tasks;
using UnityEngine;

public class MobileMeetingPointCommandExecutor : MeetingPointCommandExecutor
{
    #region Fields

    private Vector3 position;

    #endregion

    #region Unity events

    protected void Awake()
    {
        position = _pivotPoint.position;
    }

    private void Update()
    {
        _pivotPoint.position = position;
    }

    #endregion

    #region Base methods

    public async override Task ExecuteSpecificCommand(IRendezvousPointCommand command)
    {
        position = command.Target;
    }

    #endregion
}