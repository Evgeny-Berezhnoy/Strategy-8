using UnityEngine;

public interface IProduceUnitCommand : ICommand
{

    #region Properties

    GameObject UnitPrefab { get; }

    #endregion

}