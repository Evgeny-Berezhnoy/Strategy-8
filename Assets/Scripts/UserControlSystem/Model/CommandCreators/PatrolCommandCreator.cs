using UnityEngine;

public class PatrolCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
{

    #region Base Methods

    protected override IPatrolCommand CreateCommand(Vector3 argument)
    {
        return new PatrolCommand(argument);
    }

    #endregion

}