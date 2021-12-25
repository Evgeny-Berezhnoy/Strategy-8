using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{

    #region Fields

    [InjectAsset("Zergling")]private GameObject _unitPrefab;

    #endregion

    #region Interfaces Properties

    public GameObject UnitPrefab => _unitPrefab;

    #endregion

}