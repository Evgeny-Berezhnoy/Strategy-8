using UnityEngine;

public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
{

    #region Base Methods

    protected override IMoveCommand CreateCommand(Vector3 argument)
    {
        return new MoveCommand(argument);
    }

    #endregion

}