using UnityEngine;
using UniRx;

public class AutoAttackAgent : MonoBehaviour
{
    #region Fields

    private CommandExecutorBase<IAttackCommand> _attacker;
    private UnitCommandQueue _queue;
    
    #endregion

    #region Unity events

    private void Awake()
    {
        _attacker   = GetComponent<AttackCommandExecutor>();
        _queue      = GetComponent<UnitCommandQueue>();

        AutoAttackEvaluator
            .AUTO_ATTACKS_COMMANDS
            .ObserveOnMainThread()
            .Where(command => command.Attacker == gameObject)
            .Where(command => command.Attacker != null && command.Target != null)
            .Subscribe(command => AutoAttack(command.Target))
            .AddTo(this);
    }

    #endregion

    #region Methods

    private void AutoAttack(GameObject target)
    {
        _queue.Clear();
        _queue.Enqueue(_attacker, new AutoAttackCommand(target.GetComponent<IAttackable>()));
    }

    #endregion
}