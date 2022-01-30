using System;
using Zenject;

public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand>, ICancellableTokenManager
    where TCommand : ICommand
{

    #region Fields

    [Inject] private AssetsContext _context;
    [Inject] private IAwaitable<TArgument> _awaitableArgument;
    [Inject] private CancellationTokenManager _cancellationTokenManager;

    #endregion

    #region Properties

    public CancellationTokenManager CancellationTokenManager => _cancellationTokenManager;

    #endregion

    #region Base Methods

    protected override async void classSpecificCommandCreation(Action<TCommand> creationCallback)
    {
        try
        {
            var argument = await _awaitableArgument.WithCancellation(CancellationTokenManager.CreateToken());

            creationCallback?.Invoke(_context.Inject(CreateCommand(argument)));
        }
        catch { };
    }

    protected override void classSpecificCommandCreation(Action<TCommand> creationCallback, object argument)
    {
        creationCallback?.Invoke(_context.Inject(CreateCommand((TArgument)argument)));
    }

    public override void ProcessCancel()
    {
        base.ProcessCancel();

        if(CancellationTokenManager != null)
        {
            CancellationTokenManager.CancelToken();
        };
    }

    #endregion

    #region Methods

    protected abstract TCommand CreateCommand(TArgument argument);

    #endregion

}