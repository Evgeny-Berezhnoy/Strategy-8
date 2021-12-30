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

    private NavMeshAgent _agent;

    #endregion

    #region Unity Events

    private void Start()
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

    public class StopAwaiter : AwaiterBase<UnitMovementStop, Void>
    {

        #region Constructors

        public StopAwaiter(UnitMovementStop baseObject) : base(baseObject)
        {
            _baseObject.OnStop += OnStop;
        }

        #endregion

        #region Interfaces Methods

        public override Void GetResult()
        {
            return new Void();
        }

        #endregion

        #region Methods

        private void OnStop()
        {
            _baseObject.OnStop -= OnStop;

            OnBreak();
        }

        #endregion

    }

    #endregion

}