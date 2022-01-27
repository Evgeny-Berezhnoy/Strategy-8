using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CommandButtonsModel
{

    #region Events

    public event Action<ICommandExecutor> OnCommandAccepted;
    public event Action OnCommandSent;
    public event Action OnCommandCancel;

    #endregion

    #region Fields

    [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
    [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
    [Inject] private CommandCreatorBase<IStopCommand> _stopper;
    [Inject] private CommandCreatorBase<IMoveCommand> _mover;
    [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
    [Inject] private CommandCreatorBase<IRendezvousPointCommand> _rendezvousPointer;
    
    private bool _commandIsPending;

    #endregion

    #region Methods

    public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandQueueManager commandQueueManager)
    {
        
        if (_commandIsPending)
        {

            ProcessOnCancel();
        
        };

        _commandIsPending = true;

        OnCommandAccepted?.Invoke(commandExecutor);

        _unitProducer
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));
        
        _attacker
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));
        
        _stopper
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));
        
        _mover
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));
        
        _patroller
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));

        _rendezvousPointer
            .ProcessCommandExecutor(commandExecutor, command => executeCommandWrapper(commandExecutor, commandQueueManager, command));

    }

    public void executeCommandWrapper(ICommandExecutor commandExecutor, ICommandQueueManager commandQueueManager, object command)
    {

        if (commandQueueManager != null)
        {
            if (command is IStopCommand
                || !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                commandQueueManager.Clear();
            };

            commandQueueManager.Enqueue(commandExecutor, command);
        }
        else
        {
            commandExecutor.ExecuteCommand(command);
        };
        
        _commandIsPending = false;

        OnCommandSent?.Invoke();

    }

    public void OnSelectionChanged()
    {

        _commandIsPending = false;

        ProcessOnCancel();

    }
    
    private void ProcessOnCancel()
    {

        _unitProducer
            .ProcessCancel();

        _attacker
            .ProcessCancel();

        _stopper
            .ProcessCancel();

        _mover
            .ProcessCancel();

        _patroller
            .ProcessCancel();

        _rendezvousPointer
            .ProcessCancel();

        OnCommandCancel?.Invoke();

    }

    #endregion

}