public class AutoAttackCommand : IAttackCommand
{
    #region Interface Properties

    public IAttackable Target { get; private set; }

    #endregion

    #region Constructors

    public AutoAttackCommand(IAttackable target)
    {
        Target = target;
    }

    #endregion
}