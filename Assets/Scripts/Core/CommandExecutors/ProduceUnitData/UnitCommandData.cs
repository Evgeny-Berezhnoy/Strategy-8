using UnityEngine;

public class UnitCommandData: MonoBehaviour, IProduceUnitCommandData
{
    #region Fields

    [SerializeField] private float _productionTime;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _unitName;
    [SerializeField] private GameObject _unitPrefab;

    #endregion

    #region Interfaces properties

    public float ProductionTime => _productionTime;
    public Sprite Icon => _icon;
    public string UnitName => _unitName;
    public GameObject UnitPrefab => _unitPrefab;

    #endregion
}