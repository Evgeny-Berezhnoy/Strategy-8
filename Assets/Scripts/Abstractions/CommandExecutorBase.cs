using System.Threading.Tasks;
using UnityEngine;

public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor
    where T : ICommand
{

    #region Interfaces Methods

    public async Task<Husk> ExecuteCommand(object command)
    {

        await ExecuteSpecificCommand((T)command);

        return new Husk();

    }

    #endregion

    #region Methods

    public async virtual Task ExecuteSpecificCommand(T command) { }

    #endregion

}