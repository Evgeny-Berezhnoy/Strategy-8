using System;
using System.Threading.Tasks;
using Zenject;

public class StopCommandExecutor : CommandExecutorBase<IStopCommand>, ICancellableTokenManager
{

    #region Fields

    [Inject] private CancellationTokenManager _cancellationTokenManager;

    #endregion

    #region Interfaces Properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Base Methods

    public async override Task ExecuteSpecificCommand(IStopCommand command)
    {
        try
        {
            CancellationTokenManager.CancelToken();
        }
        finally { };

    }

    #endregion

}