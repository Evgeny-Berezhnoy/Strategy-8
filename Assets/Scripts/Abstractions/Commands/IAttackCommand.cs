public interface IAttackCommand : ICommand
{

    #region Fields

    IAttackable Target { get; }

    #endregion

}