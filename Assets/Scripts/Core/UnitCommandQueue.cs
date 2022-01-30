using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class UnitCommandQueue : MonoBehaviour, ICommandQueueManager, ICancellableTokenManager
{

    #region Fields

    private Queue<ExecutorWrapper> _commandQueue;

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    #endregion

    #region Interfaces properties

    public Queue<ExecutorWrapper> CommandQueue => _commandQueue;
    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Unity events

    private void Awake()
    {
        
        _commandQueue = new Queue<ExecutorWrapper>();

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
            try
            {
                _cancellationTokenManager.CancelToken();
            }
            catch { };
        };
    }

    #endregion

    #region Methods

    private async void ExecuteAction(ExecutorWrapper executorWrapper)
    {
        try
        {
            await executorWrapper.CommandExecutor.ExecuteCommand(executorWrapper.Command);

            _cancellationTokenManager.CancelToken();
        }
        finally 
        { };

    }

    #endregion

}