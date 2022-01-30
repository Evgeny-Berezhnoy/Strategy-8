using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>, ICancellableTokenManager
{

    #region Fields

    [Inject] private IHealthHolder _ourHealth;
    [Inject] private CancellationTokenManager _cancellationTokenManager;
    [Inject(Id = "AttackDistance")] private float _atttackingDistance;
    [Inject(Id = "AttackPeriod")] private int _atttackingPeriod;
    
    private Vector3 _ourPosition;
    private Vector3 _targetPosition;
    private Quaternion _ourRotation;

    private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
    private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
    private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();

    private Transform _targetTransform;
    private AttackOperation _currentAttackOperation;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private IDamageDealer _damageDealer;
    
    #endregion

    #region Interfaces properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity events

    private void Awake()
    {
        _animator               = GetComponent<Animator>();
        _navMeshAgent           = GetComponent<NavMeshAgent>();
        _damageDealer           = GetComponent<IDamageDealer>();
    }

    private void Update()
    {
        OnUpdate();
    }

    #endregion

    #region Base Methods

    public async override Task ExecuteSpecificCommand(IAttackCommand command)
    {
        _targetTransform = (command.Target as Component).transform;

        _currentAttackOperation = new AttackOperation(this, command.Target);

        OnUpdate();

        try
        {
            await _currentAttackOperation.WithCancellation(_cancellationTokenManager.CreateToken());
        }
        catch
        {
            _currentAttackOperation.Cancel();
        }
        finally
        {
            _animator.SetTrigger(AnimationTypes.IDLE);

            _navMeshAgent.destination = transform.position;

            _targetTransform = null;
            _currentAttackOperation = null;

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
            .Subscribe(SetAttackRotation)
            .AddTo(this);

        _attackTargets
            .ObserveOnMainThread()
            .Subscribe(StartAttackingTargets)
            .AddTo(this);

    }

    private void OnUpdate()
    {
        if (_currentAttackOperation == null) return;

        lock (this)
        {
            _ourPosition = transform.position;
            _ourRotation = transform.rotation;

            if(_targetTransform != null)
            {
                _targetPosition = _targetTransform.position;
            };
        };
    }

    private void StartMovingToPosition(Vector3 target)
    {
        _navMeshAgent.destination = target;
        _animator.SetTrigger(AnimationTypes.WALK);
    }

    private void SetAttackRotation(Quaternion target)
    {
        transform.rotation = target;
    }

    private void StartAttackingTargets(IAttackable target)
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        _animator.SetTrigger(AnimationTypes.ATTACK);

        target.RecieveDamage(_damageDealer.Damage);
    }

    #endregion

}