using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;

    #endregion

    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
    {

        creationCallback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));

    }

    #endregion

}