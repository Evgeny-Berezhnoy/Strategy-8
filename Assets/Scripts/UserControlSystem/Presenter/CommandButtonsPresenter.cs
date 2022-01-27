using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CommandButtonsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private CommandButtonsView _view;

    [Inject] private SelectableValue _selectable;
    [Inject] private PointableValue _pointable;
    [Inject] private CommandButtonsModel _model;

    private ISelectable _currentSelectable;
    
    #endregion

    #region UnityEvents

    private void Start()
    {
        _view.OnClick               += _model.OnCommandButtonClicked;

        _model.OnCommandSent        += _view.UnblockAllInteractions;
        _model.OnCommandCancel      += _view.UnblockAllInteractions;
        _model.OnCommandAccepted    += _view.BlockInteractions;

        _selectable.Subscribe(gameObject, onSelected);
        
        onSelected(_selectable.CurrentValue);
    }

    #endregion

    #region Methods

    private void onSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable) return;
    
        if(_currentSelectable != null)
        {
            _model.OnSelectionChanged();
        };

        _currentSelectable = selectable;

        _view.Clear();
        
        if (selectable != null)
        {
            var commandExecutors = new List<ICommandExecutor>();
            
            commandExecutors.AddRange((selectable as Component)?.GetComponentsInParent<ICommandExecutor>());

            var commandQueueManager = (selectable as Component)?.GetComponentInParent<ICommandQueueManager>();

            _view.MakeLayout(commandExecutors, commandQueueManager);

        };
    }

    #endregion

}