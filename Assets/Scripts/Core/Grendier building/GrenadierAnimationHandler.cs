using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class GrenadierAnimationHandler : MonoBehaviour
{
    #region Static fields

    private static readonly string WALK         = "Walk";
    private static readonly string PRODUCTION   = "Production";
    private static readonly string DIE          = "Die";

    #endregion

    #region Fields

    [Inject(Id = "Death duration")] private float _deathDuration;

    private Animator _animator;

    #endregion

    #region Unity events

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    #endregion

    #region Methods

    public void Die()
    {
        _animator.SetTrigger(DIE);

        Destroy(gameObject, _deathDuration);
    }

    public void StartWalking()
    {
        _animator.SetBool(WALK, true);
    }

    public void StopWalking()
    {
        _animator.SetBool(WALK, false);
    }

    public void StartProducingUnit()
    {
        _animator.SetBool(PRODUCTION, true);
    }

    public void StopProducingUnit()
    {
        _animator.SetBool(PRODUCTION, false);
    }

    #endregion
}