using UnityEngine;

[RequireComponent(typeof(GrenadierAnimationHandler))]
public class GrenadierProduceUnitCommandExecutor : ProduceUnitCommandExecutor
{
    #region Fields

    private GrenadierAnimationHandler _animationHandler;

    #endregion

    #region Base methods

    protected override void Awake()
    {
        base.Awake();

        _animationHandler = GetComponent<GrenadierAnimationHandler>();
    }

    protected new void Update()
    {
        if (_queue.Count == 0) return;

        var innerTask = (UnitProductionTask)_queue[0];

        innerTask.TimeLeft -= Time.deltaTime;

        if (innerTask.TimeLeft <= 0)
        {
            var unit =
                _diContainer.InstantiatePrefab(
                    innerTask.UnitPrefab,
                    new Vector3(_instantiationPoint.position.x, 0, _instantiationPoint.position.z),
                    Quaternion.identity,
                    _unitParent);

            var moveCommandExecutor = unit.GetComponentInParent<CommandExecutorBase<IMoveCommand>>();

            if (moveCommandExecutor)
            {
                _mover.ProcessCommandExecutor(moveCommandExecutor, async (command) => await moveCommandExecutor.ExecuteCommand(command), _pivotPoint.position);
            };

            var unitFactionMember = unit.GetComponent<IFactionMember>();

            unitFactionMember.SetFaction(_factionMember.FactionID);

            removeTaskAtIndex(0);

            if(_queue.Count > 0)
            {
                _animationHandler.StartProducingUnit();
            }
            else
            {
                _animationHandler.StopProducingUnit();
            };
        }
        else
        {
            _animationHandler.StartProducingUnit();
        };
    }

    #endregion
}