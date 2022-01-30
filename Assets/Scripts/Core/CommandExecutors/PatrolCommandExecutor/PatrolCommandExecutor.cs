using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public partial class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>, ICancellableTokenManager
{

    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;
    [Inject(Id = "ReachingDistance")] private float _reachingDistance;
    [Inject(Id = "FluctuationDistance")] private float _fluctuationDistance;

    private Vector3 _targetPosition;
    private Vector3 _targetPatrolPosition;
    private Vector3 _ourPosition;
    private Quaternion _ourRotation;

    private readonly Subject<bool> _targetAnimation = new Subject<bool>();
    private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
    private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();

    private PatrolOperation _currentOperation;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    #endregion

    #region Interfaces properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity events

    private void Awake()
    {
        _animator       = GetComponent<Animator>();
        _navMeshAgent   = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        OnUpdate();
    }

    #endregion

    #region Base Methods

    public async override Task ExecuteSpecificCommand(IPatrolCommand command)
    {
        _targetPosition         = command.Target;
        _targetPatrolPosition   = command.Target;

        _currentOperation = new PatrolOperation(this);

        OnUpdate();

        try
        {
            await _currentOperation.WithCancellation(_cancellationTokenManager.CreateToken());
        }
        catch
        {
            _currentOperation.Cancel();
        }
        finally
        {
            _animator.SetTrigger(AnimationTypes.IDLE);

            _navMeshAgent.destination = transform.position;
            
            _targetPosition = _ourPosition;
            _targetPatrolPosition   = _ourPosition;

            _currentOperation       = null;

            _cancellationTokenManager.CancelToken();
        };
    }

    #endregion

    #region Methods

    [Inject]
    private void Init()
    {

        _targetPositions
            .Select(value =>
                new Vector3(
                    (float)Math.Round(value.x),
                    (float)Math.Round(value.y),
                    (float)Math.Round(value.z)))
            .Distinct()
            .ObserveOnMainThread()
            .Subscribe(StartMovingToPosition)
            .AddTo(this);

        _targetRotations
            .ObserveOnMainThread()
            .Subscribe(SetPatrolRotation)
            .AddTo(this);

        _targetAnimation
            .ObserveOnMainThread()
            .Subscribe(SetPatrolMovingAnimation)
            .AddTo(this);

    }

    private void OnUpdate()
    {
        if (_currentOperation == null) return;

        lock (this)
        {
            _ourPosition = transform.position;
            _ourRotation = transform.rotation;
        };
    }

    private void StartMovingToPosition(Vector3 target)
    {
        _targetPatrolPosition       = target;

        _navMeshAgent.destination   = target; 
    }

    private void SetPatrolRotation(Quaternion target)
    {
        transform.rotation = target;
    }

    private void SetPatrolMovingAnimation(bool isWalking)
    {
        if (isWalking)
        {
            _animator.SetTrigger(AnimationTypes.WALK);
        }
        else
        {
            _animator.SetTrigger(AnimationTypes.IDLE);
        };
    }

    #endregion

}