using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandButtonsPresenter : MonoBehaviour
{

    #region Fields

    [SerializeField] private SelectableValue _selectable;
    [SerializeField] private CommandButtonsView _view;
    [SerializeField] private AssetsContext _context;

    private ISelectable _currentSelectable;
    
    #endregion

    #region UnityEvents

    private void Start()
    {

        _selectable.OnSelected += onSelected;
        
        onSelected(_selectable.CurrentValue);

        _view.OnClick += onButtonClick;
    
    }

    #endregion

    #region Methods

    private void onSelected(ISelectable selectable)
    {

        if (_currentSelectable == selectable)
        {

            return;
        
        }
        
        _currentSelectable = selectable;

        _view.Clear();
        
        if (selectable != null)
        {
            
            var commandExecutors = new List<ICommandExecutor>();
            
            commandExecutors.AddRange((selectable as Component)?.GetComponentsInParent<ICommandExecutor>());
            
            _view.MakeLayout(commandExecutors);
        
        }

    }

    private void onButtonClick(ICommandExecutor commandExecutor)
    {
        
        if (commandExecutor is CommandExecutorBase<IProduceUnitCommand> unitProducer)
        {
        
            unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommandHeir()));
            
            return;
        
        }
        else if (commandExecutor is CommandExecutorBase<IAttackCommand> attacker)
        {

            attacker.ExecuteSpecificCommand(new AttackCommand());

            return;

        }
        else if(commandExecutor is CommandExecutorBase<IMoveCommand> mover)
        {

            mover.ExecuteSpecificCommand(new MoveCommand());

            return;

        }
        else if(commandExecutor is CommandExecutorBase<IPatrolCommand> patroler)
        {

            patroler.ExecuteSpecificCommand(new PatrolCommand());

            return;

        }
        else if(commandExecutor is CommandExecutorBase<IStopCommand> stopper)
        {

            stopper.ExecuteSpecificCommand(new StopCommand());

            return;

        };

            throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(onButtonClick)}: Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
    
    }
    
    #endregion

}