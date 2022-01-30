using UnityEngine;

public class RendezvousPointCommandCreator : CancellableCommandCreatorBase<IRendezvousPointCommand, Vector3>
{

    #region Base methods

    protected override IRendezvousPointCommand CreateCommand(Vector3 argument)
    {
        return new RendezvousPointCommand(argument);
    }

    #endregion

}