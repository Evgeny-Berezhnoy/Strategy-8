public interface IAttackCommand : ICommand
{

    #region Fields

    IPointable Target { get; }

    #endregion

}