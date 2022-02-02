using UnityEngine;

public class UnitProductionTask : IUnitProductionTask
{
    #region Interfaces properties
    
    public string UnitName { get; }
    public float TimeLeft { get; set; }
    public Transform PivotPoint { get; }
    public float ProductionTime { get; }
    public Sprite Icon { get; }
    public GameObject UnitPrefab { get; }
    
    #endregion

    #region Constructors

    public UnitProductionTask(float time, Sprite icon, GameObject unitPrefab, string unitName, Transform pivotPoint)
    {
        Icon            = icon;
        ProductionTime  = time;
        PivotPoint      = pivotPoint;
        TimeLeft        = time;
        UnitPrefab      = unitPrefab;
        UnitName        = unitName;
    }
    
    #endregion
}