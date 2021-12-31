using System;
using Zenject;

public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;

    private Action<IAttackCommand> _creationCallback;

    #endregion
    
    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IAttackCommand> creationCallback)
    {

        _creationCallback = creationCallback;

    }

    public override void ProcessCancel()
    {

        base.ProcessCancel();

        _creationCallback = null;

    }

    #endregion

    #region Methods


    [Inject]
    private void Init(PointableValue pointable)
    {

        pointable.OnNewValue += onNewValue;

    }

    private void onNewValue(IPointable pointable)
    {

        _creationCallback?.Invoke(_context.Inject(new AttackCommand(pointable)));
        _creationCallback = null;

    }

    #endregion

}