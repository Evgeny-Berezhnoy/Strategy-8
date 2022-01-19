using System;
using Zenject;

public class StopCommandCreator : CommandCreatorBase<IStopCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;

    #endregion

    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IStopCommand> creationCallback)
    {

        creationCallback?.Invoke(_context.Inject(new StopCommand()));

    }

    #endregion

}