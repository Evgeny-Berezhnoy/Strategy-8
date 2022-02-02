using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CommandButtonsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private CommandButtonsView _view;

    [Inject] private SelectableValue _selectable;
    [Inject] private AttackableValue _attackable;
    [Inject] private Vector3Value _pointable;
    [Inject] private CommandButtonsModel _model;

    private ISelectable _currentSelectable;

    private event Action<Vector3> _onMoveCommandLaunch;

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

        _pointable.Subscribe(OnPointed);
    }

    private void OnDestroy()
    {
        ClearMoveCommandLaunchEvent();
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

        ClearMoveCommandLaunchEvent();

        if (selectable != null)
        {
            var commandExecutors = new List<ICommandExecutor>();

            var selectableComponent = (selectable as Component);

            commandExecutors.AddRange(selectableComponent?.GetComponentsInParent<ICommandExecutor>());

            var commandQueueManager = selectableComponent?.GetComponentInParent<ICommandQueueManager>();

            _view.MakeLayout(commandExecutors, commandQueueManager);

            var moveCommandExecutorBase = selectableComponent?.GetComponentInParent<CommandExecutorBase<IMoveCommand>>();

            if(moveCommandExecutorBase is IAutomaticalMoveCommandAcceptor)
            {
                _onMoveCommandLaunch += ((position) => _model.OnPositionPointed(moveCommandExecutorBase, commandQueueManager, position));
            };
        };
    }

    private void OnPointed(Vector3 position)
    {
        _onMoveCommandLaunch?.Invoke(position);
    }

    private void ClearMoveCommandLaunchEvent()
    {
        var handlers =
            _onMoveCommandLaunch?
            .GetInvocationList()
            .ToList()
            .Cast<Action<Vector3>>()
            .ToList();

        if (handlers == null) return;

        for(int i = 0; i < handlers.Count; i++)
        {
            _onMoveCommandLaunch -= handlers[i];
        };
    }

    #endregion

}