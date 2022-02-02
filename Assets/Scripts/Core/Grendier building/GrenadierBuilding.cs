using UnityEngine;

[RequireComponent(typeof(GrenadierAnimationHandler))]
public class GrenadierBuilding : Building
{
    #region Fields

    private GrenadierAnimationHandler _animationHandler;

    #endregion

    #region Unity events

    protected override void Awake()
    {
        base.Awake();

        _animationHandler = GetComponent<GrenadierAnimationHandler>();
    }

    #endregion

    #region Base methods

    public override void RecieveDamage(int amount)
    {
        if (_health <= 0) return;

        _health -= amount;

        if (_health <= 0)
        {
            _animationHandler.Die();
        };
    }

    #endregion
}