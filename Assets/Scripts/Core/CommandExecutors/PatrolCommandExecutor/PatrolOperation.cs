using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

using Random    = System.Random;
using Void      = AsyncExtensions.Void;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public partial class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>, ICancellableTokenManager
{

    #region Nested classes

    private class PatrolOperation : IAwaitable<Void>
    {

        #region Events

        private event Action OnCompleted;

        #endregion

        #region Fields

        private bool _isCancelled;

        private readonly PatrolCommandExecutor _commandExecutor;
        private readonly float _fluctuationDistance;

        #endregion

        #region Constructors

        public PatrolOperation(PatrolCommandExecutor commandExecutor)
        {
            _commandExecutor        = commandExecutor;
            _fluctuationDistance    = commandExecutor._fluctuationDistance;

            SetWalkingAnimation();

            ThreadPool.QueueUserWorkItem(ExecuteAlgorythm);
        }

        #endregion

        #region Interfaces methods

        public IAwaiter<Void> GetAwaiter()
        {
            return new OperationAwaiter(this);
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

                if (ThreadIsCancelled()) return;
                
                var targetPosition          = default(Vector3);
                var targetPatrolPosition    = default(Vector3);
                var ourPosition             = default(Vector3);
                var ourRotation             = default(Quaternion);

                lock (_commandExecutor)
                {
                    targetPosition          = _commandExecutor._targetPosition;
                    targetPatrolPosition    = _commandExecutor._targetPatrolPosition;
                    ourPosition             = _commandExecutor._ourPosition;
                    ourRotation             = _commandExecutor._ourRotation;
                };

                var vector              = targetPatrolPosition - ourPosition;
                var distanceToTarget    = vector.magnitude;
                var lookRotation        = Quaternion.LookRotation(vector);

                if (distanceToTarget > _commandExecutor._reachingDistance)
                {
                    var finalDestination = targetPatrolPosition - vector.normalized * (_commandExecutor._reachingDistance * 0.9f);

                    _commandExecutor
                        ._targetPositions
                        .OnNext(finalDestination);

                    SetWalkingAnimation();

                    Thread.Sleep(500);
                }
                else if (ourRotation != lookRotation)
                {
                    _commandExecutor
                        ._targetRotations
                        .OnNext(lookRotation);
                }
                else
                {
                    SetIdleAnimation();

                    Thread.Sleep(2000);

                    if (ThreadIsCancelled()) return;

                    var random = new Random();

                    var finalDestination =
                        new Vector3(
                            targetPosition.x + (float)random.NextDouble() * _fluctuationDistance,
                            targetPosition.y,
                            targetPosition.z + (float)random.NextDouble() * _fluctuationDistance);

                    _commandExecutor
                        ._targetPositions
                        .OnNext(finalDestination);

                    SetWalkingAnimation();

                };
            };
        }

        private bool ThreadIsCancelled()
        {
            if (_commandExecutor == null || _isCancelled)
            {
                OnCompleted?.Invoke();
                OnCompleted -= SetIdleAnimation;

                return true;
            };

            return false;
        }

        private void SetWalkingAnimation()
        {
            if (_commandExecutor != null)
            {
                _commandExecutor
                    ._targetAnimation
                    .OnNext(true);
            };
        }

        private void SetIdleAnimation()
        {
            if(_commandExecutor != null)
            {
                _commandExecutor
                    ._targetAnimation
                    .OnNext(false);
            };
        }

        #endregion

        #region Nested classes

        public class OperationAwaiter : AwaiterBase<PatrolOperation, Void>
        {

            #region Constructors

            public OperationAwaiter(PatrolOperation baseObject) : base(baseObject)
            {
                _baseObject.OnCompleted += OnBreak;

                _result = new Void();
            }

            #endregion

            #region Base methods

            protected override void OnBreak()
            {
                base.OnBreak();
                
                _baseObject.OnCompleted -= OnBreak;
            }

            #endregion

        }

        #endregion

    }

    #endregion

}