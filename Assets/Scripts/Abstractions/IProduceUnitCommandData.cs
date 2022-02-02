using UnityEngine;

public interface IProduceUnitCommandData
{
    #region Properties

    float ProductionTime { get; }
    Sprite Icon { get; }
    string UnitName { get; }
    GameObject UnitPrefab { get; }

    #endregion
}