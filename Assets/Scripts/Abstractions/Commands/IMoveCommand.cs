using UnityEngine;

public interface IMoveCommand : ICommand
{

    #region Properties

    Vector3 Target { get; }

    #endregion

}