using UnityEngine;

public class PatrolCommand : IPatrolCommand
{

    #region Interfaces Properties

    public Vector3 Target { get; }

    #endregion

    #region Constructors

    public PatrolCommand(Vector3 target)
    {

        Target = target;

    }

    #endregion

}