using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitMovementStop))]
public class UnitMoveCommandExecutor : CommandExecutorBase<IMoveCommand>, ICancellableTokenManager
{

    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private UnitMovementStop _stop;
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");

    #endregion

    #region Interfaces Properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity Events

    private void Awake()
    {
        _navMeshAgent   = GetComponent<NavMeshAgent>();
        _animator       = GetComponent<Animator>();
        _stop           = GetComponent<UnitMovementStop>();
    }

    #endregion

    #region Base Methods

    public override async Task ExecuteSpecificCommand(IMoveCommand command)
    {
        _navMeshAgent.destination = command.Target;
        _animator.SetTrigger(Walk);

        try
        {
            await _stop.WithCancellation(CancellationTokenManager.CreateToken());
        }
        catch
        {

        }
        finally
        {
            _navMeshAgent.destination = transform.position;
            _animator.SetTrigger(Idle);

            CancellationTokenManager.CancelToken();
        };

    }

    #endregion

}