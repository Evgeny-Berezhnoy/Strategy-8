using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UnitCommandQueue : MonoBehaviour, ICommandQueueManager
{

    #region Fields

    private Queue<ExecutorWrapper> _commandQueue;

    private CancellationTokenManager _cancellationTokenManager;

    #endregion

    #region Interfaces properties

    public Queue<ExecutorWrapper> CommandQueue => _commandQueue;

    #endregion

    #region Unity events

    private void Awake()
    {
        
        _commandQueue               = new Queue<ExecutorWrapper>();
        _cancellationTokenManager   = new CancellationTokenManager();

    }

    private void Update()
    {

        if (_cancellationTokenManager.TokenIsActive
            || _commandQueue.Count == 0)
        {
            return;
        };

        ExecuteAction(_commandQueue.Dequeue());
    }

    private void OnDestroy()
    {
        Clear();
    }

    #endregion

    #region Interfaces methods

    public void Enqueue(ICommandExecutor commandExecutor, object command)
    {
        _commandQueue.Enqueue(new ExecutorWrapper() { CommandExecutor = commandExecutor, Command = command });
    }

    public void Clear()
    {
        _commandQueue.Clear();
        
        if (_cancellationTokenManager.TokenIsActive)
        {
            _cancellationTokenManager.CancelToken();
        };
    }

    #endregion

    #region Methods

    private async void ExecuteAction(ExecutorWrapper executorWrapper)
    {
        try
        {
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            await 
                Task.Factory
                .StartNew(async () =>
                {

                    await executorWrapper.CommandExecutor.ExecuteCommand(executorWrapper.Command);

                    _cancellationTokenManager.CancelToken();

                }, _cancellationTokenManager.CreateToken(), TaskCreationOptions.None, taskScheduler);

        }
        finally 
        { };
    }

    #endregion

}