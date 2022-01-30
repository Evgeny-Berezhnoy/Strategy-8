using UnityEngine;

[RequireComponent(typeof(OutlineDraw))]
public class MainBuilding : MonoBehaviour, ISelectable, IOutlinable, IAttackable
{

    #region Fields

    [SerializeField] private Transform _unitParent;
    [SerializeField] private int _maxHealth = 1000;
    [SerializeField] private Sprite _icon;
    [SerializeField] private OutlineDraw _outlineDraw;
    
    private int _health = 100;

    #endregion

    #region Interfaces Properties

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public OutlineDraw OutlineDraw => _outlineDraw;

    #endregion

    #region Interfaces Methods


    public void RecieveDamage(int amount)
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