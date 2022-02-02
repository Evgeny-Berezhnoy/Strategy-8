using System;

public abstract class CommandCreatorBase<T>
    where T : ICommand
{

    #region Methods

    protected virtual void classSpecificCommandCreation(Action<T> creationCallback) { }

    protected virtual void classSpecificCommandCreation(Action<T> creationCallback, object argument)
    {
        classSpecificCommandCreation(creationCallback);
    }

    public virtual ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
    {
        var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;

        if(classSpecificExecutor != null)
        {
            classSpecificCommandCreation(callback);
        };

        return commandExecutor;
    }

    public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback, object argument)
    {
        var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;

        if (classSpecificExecutor != null)
        {
            classSpecificCommandCreation(callback, argument);
        };

        return commandExecutor;
    }
    
    public virtual void ProcessCancel() { }
    
    #endregion

}