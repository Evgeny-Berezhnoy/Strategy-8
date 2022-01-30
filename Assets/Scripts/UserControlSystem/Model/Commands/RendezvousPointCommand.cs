using UnityEngine;

public class RendezvousPointCommand : IRendezvousPointCommand
{

    #region Interface Properties

    public Vector3 Target { get; private set; }

    #endregion

    #region Constructors

    public RendezvousPointCommand(Vector3 target)
    {
        Target = target;
    }

    #endregion

}