using System;
using UnityEngine;
using Zenject;

public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;

    private Action<IMoveCommand> _creationCallback;
    
    #endregion

    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IMoveCommand> creationCallback)
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
    private void Init(Vector3Value groundClicks)
    {

        groundClicks.OnNewValue += onNewValue;

    }

    private void onNewValue(Vector3 groundClick)
    {

        _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClick)));
        _creationCallback = null;

    }

    #endregion

}