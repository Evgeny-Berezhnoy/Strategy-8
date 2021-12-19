using UnityEngine;

[RequireComponent(typeof(OutlineDraw))]
public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IOutlinable
{

    #region Fields

    [SerializeField] private Transform _unitsParent;

    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;

    [SerializeField] private OutlineDraw _outlineDraw;

    private float _health = 1000;

    #endregion

    #region Interfaces Properties

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;
    
    #endregion

    #region Base Methods

    public override void ExecuteSpecificCommand(IProduceUnitCommand command)
    {

        Instantiate(
            command.UnitPrefab,
            new Vector3(
                Random.Range(-10, 10),
                0,
                Random.Range(-10, 10)),
                Quaternion.identity,
                _unitsParent);

    }

    #endregion

}