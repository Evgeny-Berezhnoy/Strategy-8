using System;
using UnityEngine;
using UnityEngine.AI;

using Void = AsyncExtensions.Void;

[RequireComponent(typeof(NavMeshAgent))]
public class GrenadierMovementProcessor : MonoBehaviour, IAwaitable<Void>
{
    #region Events

    public event Action OnStop;

    #endregion

    #region Fields

    private NavMeshAgent _agent;
    
    #endregion

    #region Unity Events

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!_agent.pathPending
                && _agent.remainingDistance <= _agent.stoppingDistance
                && (_agent.hasPath || _agent.velocity.sqrMagnitude == 0))
        {
            OnStop?.Invoke();
        };
    }

    #endregion

    #region Interfaces Methods

    public IAwaiter<Void> GetAwaiter()
    {
        return new StopAwaiter(this);
    }

    #endregion

    #region Nested classes

    private class StopAwaiter : AwaiterBase<GrenadierMovementProcessor, Void>
    {
        #region Constructors

        public StopAwaiter(GrenadierMovementProcessor baseObject) : base(baseObject)
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