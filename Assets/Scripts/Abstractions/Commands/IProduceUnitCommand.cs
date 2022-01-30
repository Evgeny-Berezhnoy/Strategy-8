using UnityEngine;

public interface IProduceUnitCommand : ICommand, IIconHolder
{

    #region Properties

    float ProductionTime { get; }
    GameObject UnitPrefab { get; }
    string UnitName { get; }

    #endregion

}