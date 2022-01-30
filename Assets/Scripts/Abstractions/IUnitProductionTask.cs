using UnityEngine;

public interface IUnitProductionTask : IIconHolder
{
    #region Properties

    string UnitName { get; }
    float TimeLeft { get; }
    float ProductionTime { get; }
    Transform PivotPoint { get; }

    #endregion
}