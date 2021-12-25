using UnityEngine;

public class MoveCommand : IMoveCommand
{

    #region Interface Properties

    public Vector3 Target { get; private set; }

    #endregion

    #region Constructors

    public MoveCommand(Vector3 target)
    {

        Target = target;

    }

    #endregion

}