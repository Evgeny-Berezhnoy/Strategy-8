using UnityEngine;

[RequireComponent(typeof(OutlineDraw))]
[RequireComponent(typeof(UnitMoveCommand))]
[RequireComponent(typeof(UnitStopCommand))]
public class Unit : MonoBehaviour, ISelectable, IOutlinable, IPointable
{

    #region Fields

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

    #region Unity Events

    public void Awake()
    {

        var cancellationTokenManager = new CancellationTokenManager();

        gameObject.GetComponent<UnitMoveCommand>().CancellationTokenManager = cancellationTokenManager;
        gameObject.GetComponent<UnitStopCommand>().CancellationTokenManager = cancellationTokenManager;
        
    }

    #endregion

}