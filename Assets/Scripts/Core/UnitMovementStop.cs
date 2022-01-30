using System;
using UnityEngine;
using UnityEngine.AI;

using Void = AsyncExtensions.Void;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovementStop : MonoBehaviour, IAwaitable<Void>
{

    #region Events

    public event Action OnStop;

    #endregion

    #region Fields

    [SerializeField] private Collider _collider;
    [Range(0f, 10f)] [SerializeField] private float _collisionTimeThreshold = 3;

    private NavMeshAgent _agent;

    private int _obstacleContactCount   = 0;
    private float _collisionTimeCurrent = 0;

    #endregion

    #region Unity Events

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (!_collider)
        {
            throw new MissingComponentException($"{GetType()} requires {typeof(Collider)} component set!");
        };
    }

    private void Update()
    {
        if (!_agent.pathPending
                && _agent.remainingDistance <= _agent.stoppingDistance
                && (_agent.hasPath || _agent.velocity.sqrMagnitude == 0))
        {
            OnStop?.Invoke();
        }
        else if(_agent.hasPath)
        {
            if(_obstacleContactCount == 0)
            {
                _collisionTimeCurrent = 0;
            }
            else
            {
                _collisionTimeCurrent += Time.deltaTime;

                if(_collisionTimeCurrent >= _collisionTimeThreshold)
                {
                    OnStop?.Invoke();
                };
            };
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsObstacle(collision) != null)
        {
            _obstacleContactCount++;
        };
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsObstacle(collision))
        {
            _obstacleContactCount--;
        };
    }

    #endregion

    #region Interfaces Methods

    public IAwaiter<Void> GetAwaiter()
    {
        return new StopAwaiter(this);
    }

    #endregion

    #region Methods

    private NavMeshObstacle IsObstacle(Collision collision)
    {
        return collision
                .collider
                .transform
                .parent
                .gameObject
                .GetComponentInChildren<NavMeshObstacle>();
    }

    #endregion

    #region Nested classes

    private class StopAwaiter : AwaiterBase<UnitMovementStop, Void>
    {

        #region Constructors

        public StopAwaiter(UnitMovementStop baseObject) : base(baseObject)
        {
            _baseObject.OnStop += OnStop;

            _result = new Void();
        }

        #endregion

        #region Methods

        private void OnStop()
        {
            OnBreak();

            _baseObject.OnStop -= OnStop;
        }

        #endregion

    }

    #endregion

}