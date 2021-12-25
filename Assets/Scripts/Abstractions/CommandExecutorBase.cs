using UnityEngine;

public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor
    where T : ICommand
{

    #region Interfaces Methods
    
    public void ExecuteCommand(object command)
    {

        ExecuteSpecificCommand((T)command);

    }

    #endregion

    #region Methods

    public abstract void ExecuteSpecificCommand(T command);

    #endregion

}