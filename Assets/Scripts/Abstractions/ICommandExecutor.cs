using System.Threading.Tasks;

public interface ICommandExecutor
{

    #region Methods

    Task<Husk> ExecuteCommand(object command);

    #endregion

}