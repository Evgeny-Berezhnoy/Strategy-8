using UnityEngine;

[RequireComponent(typeof(OutlineDraw))]
public class Building : MonoBehaviour, ISelectable, IOutlinable, IAttackable
{
    #region Fields

    [SerializeField] [Range(1, 100)] protected int _maxHealth;
    [SerializeField] protected Sprite _icon;

    protected int _health;
    protected OutlineDraw _outlineDraw;
    
    #endregion

    #region Interfaces Properties

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;

    #endregion

    #region Unity events

    protected virtual void Awake()
    {
        _health = _maxHealth;

        _outlineDraw = GetComponent<OutlineDraw>();
    }

    #endregion

    #region Interfaces Methods

    public virtual void RecieveDamage(int amount)
    {
        if (_health <= 0) return;
        
        _health -= amount;
        
        if (_health <= 0)
        {
            Destroy(gameObject);
        };
    }

    #endregion
}