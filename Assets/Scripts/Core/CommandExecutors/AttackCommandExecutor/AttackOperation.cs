using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

using Void = AsyncExtensions.Void;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>, ICancellableTokenManager
{

    #region Nested classes

    private class AttackOperation : IAwaitable<Void>
    {

        #region Events

        private event Action OnCompleted;

        #endregion

        #region Fields

        private bool _isCancelled;

        private readonly AttackCommandExecutor _commandExecutor;
        private readonly IAttackable _target;

        #endregion

        #region Constructors

        public AttackOperation(AttackCommandExecutor commandExecutor, IAttackable target)
        {
            _commandExecutor    = commandExecutor;
            _target             = target;

            new Thread(ExecuteAlgorythm).Start();
        }

        #endregion

        #region Interfaces methods

        public IAwaiter<Void> GetAwaiter()
        {
            return new AttackOperationAwaiter(this);
        }

        #endregion

        #region Methods

        public void Cancel()
        {
            _isCancelled = true;
            OnCompleted?.Invoke();
        }

        private void ExecuteAlgorythm(object obj)
        {
            while (true)
            {
                if (_commandExecutor == null
                    || _commandExecutor._ourHealth.Health <= 0
                    || _target.Health <= 0
                    || _isCancelled)
                {
                    OnCompleted?.Invoke();
                    
                    return;
                };

                var targetPosition  = default(Vector3);
                var ourPosition     = default(Vector3);
                var ourRotation     = default(Quaternion);

                lock (_commandExecutor)
                {
                    targetPosition  = _commandExecutor._targetPosition;
                    ourPosition     = _commandExecutor._ourPosition;
                    ourRotation     = _commandExecutor._ourRotation;
                };

                var vector              = targetPosition - ourPosition;
                var distanceToTarget    = vector.magnitude;
                var lookRotation        = Quaternion.LookRotation(vector);

                if (distanceToTarget > _commandExecutor._atttackingDistance)
                {
                    var finalDestination = targetPosition - vector.normalized * (_commandExecutor._atttackingDistance * 0.9f);

                    _commandExecutor
                        ._targetPositions
                        .OnNext(finalDestination);

                    Thread.Sleep(100);
                }
                else if(ourRotation != lookRotation)
                {
                    _commandExecutor
                        ._targetRotations
                        .OnNext(lookRotation);
                }
                else
                {
                    _commandExecutor.
                        _attackTargets
                        .OnNext(_target);

                    Thread.Sleep(_commandExecutor._atttackingPeriod);
                };
            };
        }

        #endregion

        #region Nested classes

        public class AttackOperationAwaiter : AwaiterBase<AttackOperation, Void>
        {

            #region Constructors

            public AttackOperationAwaiter(AttackOperation baseObject) : base(baseObject)
            {
                _baseObject.OnCompleted += OnBreak;

                _result = new Void();
            }

            #endregion

            #region Base methods

            protected override void OnBreak()
            {
                _baseObject.OnCompleted -= OnBreak;
            }

            #endregion

        }

        #endregion

    }

    #endregion

}