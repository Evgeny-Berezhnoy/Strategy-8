using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GrenadierMovementProcessor))]
[RequireComponent(typeof(GrenadierAnimationHandler))]
public class GrenadierMoveCommandExecutor : CommandExecutorBase<IMoveCommand>, ICancellableTokenManager
{
    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    private NavMeshAgent _navMeshAgent;
    private GrenadierMovementProcessor _movementProcessor;
    private GrenadierAnimationHandler _animationHandler;

    #endregion

    #region Interfaces Properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity Events

    private void Awake()
    {
        _navMeshAgent       = GetComponent<NavMeshAgent>();
        _movementProcessor  = GetComponent<GrenadierMovementProcessor>();
        _animationHandler   = GetComponent<GrenadierAnimationHandler>();
    }

    #endregion

    #region Base Methods

    public override async Task ExecuteSpecificCommand(IMoveCommand command)
    {
        _navMeshAgent.destination = command.Target;
        _animationHandler.StartWalking();

        try
        {
            await _movementProcessor.WithCancellation(CancellationTokenManager.CreateToken());
        }
        catch
        {

        }
        finally
        {
            _navMeshAgent.destination = transform.position;
            _animationHandler.StopWalking();

            CancellationTokenManager.CancelToken();
        };
    }

    #endregion
}