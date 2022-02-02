using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
    #region Fields

    [Inject] private AssetsContext _context;
    [Inject] private DiContainer _diContainer;

    #endregion

    #region Base Methods

    public override ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<IProduceUnitCommand> callback)
    {
        var classSpecificExecutor   = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
        var productionDataHolder    = commandExecutor as IProduceUnitCommandDataHolder;

        if (classSpecificExecutor != null
                && productionDataHolder != null)
        {
            classSpecificCommandCreation(callback, productionDataHolder.ProductionData);
        };

        return commandExecutor;
    }

    protected override void classSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback, object argument)
    {
        var productionData      = argument as IProduceUnitCommandData;

        var produceUnitCommand  = _context.Inject(new ProduceUnitCommand(productionData));

        _diContainer.Inject(produceUnitCommand);

        creationCallback?.Invoke(produceUnitCommand);
    }

    #endregion
}