using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;
    [Inject] private DiContainer _diContainer;

    #endregion

    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
    {
        var produceUnitCommand = _context.Inject(new ProduceUnitCommand());

        _diContainer.Inject(produceUnitCommand);

        creationCallback?.Invoke(produceUnitCommand);
    }

    #endregion

}