using UnityEngine;

public interface IRendezvousPointCommand : ICommand
{

    #region Properties

    Vector3 Target { get; }

    #endregion

}