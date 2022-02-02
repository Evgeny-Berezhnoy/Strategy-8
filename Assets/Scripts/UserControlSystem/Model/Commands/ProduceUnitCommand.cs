using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{
    #region Fields

    private float _productionTime;
    private Sprite _icon;
    private string _unitName;
    private GameObject _unitPrefab;

    #endregion

    #region Interfaces Properties

    public float ProductionTime => _productionTime;
    public Sprite Icon => _icon;
    public string UnitName => _unitName;
    public GameObject UnitPrefab => _unitPrefab;

    #endregion

    #region Consturctors

    public ProduceUnitCommand(IProduceUnitCommandData data)
    {
        _productionTime = data.ProductionTime;
        _icon           = data.Icon;
        _unitName       = data.UnitName;
        _unitPrefab     = data.UnitPrefab;
    }

    #endregion
}