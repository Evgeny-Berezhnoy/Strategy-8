using UnityEngine;
using UniRx;
using Zenject;
using System.Threading.Tasks;

public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
{

    #region Fields

    [SerializeField] private Transform _unitParent;
    [SerializeField] private int _maximumUnitsInQueue = 6;
    [SerializeField] private Transform _instantiationPoint;
    [SerializeField] private Transform _pivotPoint;

    [Inject] private CommandCreatorBase<IMoveCommand> _mover;
    
    private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

    #endregion

    #region Interfaces properties

    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
    public Transform PivotPoint => _pivotPoint;
    public Transform InstantiationPoint => _instantiationPoint;

    #endregion

    #region Unity events

    private void Update()
    {
        if (_queue.Count == 0) return;

        var innerTask = (UnitProductionTask) _queue[0];

        innerTask.TimeLeft -= Time.deltaTime;

        if(innerTask.TimeLeft <= 0)
        {
            var unit = Instantiate(
                            innerTask.UnitPrefab, 
                            new Vector3(_instantiationPoint.position.x, 0, _instantiationPoint.position.z),
                            Quaternion.identity,
                            _unitParent);

            var moveCommandExecutor = unit.GetComponentInParent<CommandExecutorBase<IMoveCommand>>();

            if (moveCommandExecutor)
            {
                _mover.ProcessCommandExecutor(moveCommandExecutor, command => moveCommandExecutor.ExecuteCommand(command), _pivotPoint.position);
            };

            removeTaskAtIndex(0);
        };
    }

    #endregion

    #region Interfaces methods

    public void Cancel(int index)
    {
        removeTaskAtIndex(index);
    }

    #endregion

    #region Base methods

    public async override Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        if (_queue.Count == _maximumUnitsInQueue) return;

        _queue.Add(
            new UnitProductionTask(
                command.ProductionTime,
                command.Icon,
                command.UnitPrefab,
                command.UnitName,
                _pivotPoint));
    }

    #endregion

    #region Methods
    
    private void removeTaskAtIndex(int index)
    {
        for(int i = index; i < _queue.Count - 1; i++)
        {
            _queue[i] = _queue[i + 1];
        };

        _queue.RemoveAt(_queue.Count - 1);
    }

    #endregion

}