using UnityEngine;

public interface IPatrolCommand : ICommand
{

    #region Properties

    Vector3 Target { get; }

    #endregion

}