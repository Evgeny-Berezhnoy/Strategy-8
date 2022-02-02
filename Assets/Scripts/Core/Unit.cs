using UnityEngine;
using Zenject;

[RequireComponent(typeof(OutlineDraw))]
[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour, ISelectable, IOutlinable, IAttackable, IDamageDealer, ICancellableTokenManager, IAutomaticAttacker
{

    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    [SerializeField] [Range(0, 100)] private int _maxHealth;
    [SerializeField] [Range(0, 50)] private int _damage = 25;
    [SerializeField] [Range(0, 10)] private float _visionRadius = 5f;
    [SerializeField] private Sprite _icon;
    
    private int _health;

    private Animator _animator;
    private OutlineDraw _outlineDraw;

    #endregion

    #region Interfaces Properties

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;
    public int Damage => _damage;
    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;
    public float VisionRadius => _visionRadius;

    #endregion

    #region Unity events

    private void Awake()
    {
        _animator   = GetComponent<Animator>();

        _health     = _maxHealth;
    }

    #endregion

    #region Interfaces Methods

    public void RecieveDamage(int amount)
    {
        if (_health <= 0) return;

        _health -= amount;

        if (_health <= 0)
        {
            _animator.SetTrigger("Die");

            Invoke(nameof(Kill), 1f);
        };

    }

    #endregion

    #region Methods

    private void Kill()
    {
        _cancellationTokenManager.CancelToken();

        Destroy(gameObject);
    }

    #endregion

}