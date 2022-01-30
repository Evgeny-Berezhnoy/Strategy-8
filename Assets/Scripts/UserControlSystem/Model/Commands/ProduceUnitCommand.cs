using UnityEngine;
using Zenject;

public class ProduceUnitCommand : IProduceUnitCommand
{

    #region Fields

    [InjectAsset("Zergling")]private GameObject _unitPrefab;

    #endregion

    #region Interfaces Properties

    [Inject(Id = "Zergling")] public float ProductionTime { get; }
    [Inject(Id = "Zergling")] public Sprite Icon { get; }
    [Inject(Id = "Zergling")] public string UnitName { get; }

    public GameObject UnitPrefab => _unitPrefab;

    #endregion

}