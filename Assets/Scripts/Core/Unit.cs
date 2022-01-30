using UnityEngine;
using Zenject;

[RequireComponent(typeof(OutlineDraw))]
[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour, ISelectable, IOutlinable, IAttackable, IDamageDealer, ICancellableTokenManager
{

    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Sprite _icon;
    [SerializeField] private OutlineDraw _outlineDraw;
    [SerializeField] private int _damage = 25;

    private int _health;

    public Animator _animator;

    #endregion

    #region Interfaces Properties

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;
    public int Damage => _damage;
    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity events

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _health = _maxHealth;
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