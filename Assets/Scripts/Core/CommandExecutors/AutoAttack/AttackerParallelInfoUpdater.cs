using UnityEngine;
using Zenject;

using static AutoAttackEvaluator;

public class AttackerParallelInfoUpdater : MonoBehaviour, ITickable
{
    #region Fields

    private IAutomaticAttacker _automaticAttacker;
    private ICommandQueueManager _queue;

    #endregion

    #region Unity events

    private void Awake()
    {
        _automaticAttacker  = GetComponent<IAutomaticAttacker>();
        _queue              = GetComponent<ICommandQueueManager>();
    }

    private void OnDestroy()
    {
        ATTACKERS_INFO.TryRemove(gameObject, out _);
    }

    #endregion

    #region Interfaces methods

    public void Tick()
    {
        var attackersInfo = ATTACKERS_INFO;
        
        attackersInfo
            .AddOrUpdate(
                gameObject,
                new AttackerParallelInfo(_automaticAttacker.VisionRadius, _queue.CurrentCommand),
                (_, value) =>
                {
                    value.VisionRadius      = _automaticAttacker.VisionRadius;
                    value.CurrentCommand    = _queue.CurrentCommand;
                    return value;
                });
    }

    #endregion
}