using System;
using UnityEngine;
using Zenject;

public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
{

    #region Fields

    [Inject] private AssetsContext _context;

    private Action<IPatrolCommand> _creationCallback;

    #endregion

    #region Base Methods

    protected override void classSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
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

        _creationCallback?.Invoke(_context.Inject(new PatrolCommand(groundClick)));
        _creationCallback = null;

    }

    #endregion

}