using System;

public abstract class CommandCreatorBase<T>
    where T : ICommand
{

    #region Methods

    protected abstract void classSpecificCommandCreation(Action<T> creationCallback);

    public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
    {

        var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;

        if(classSpecificExecutor != null)
        {

            classSpecificCommandCreation(callback);

        };

        return commandExecutor;

    }

    public virtual void ProcessCancel() { }
    
    #endregion

}