public class AttackCommand : IAttackCommand
{

    #region Interface Properties

    public IAttackable Target { get; private set; }

    #endregion

    #region Constructors

    public AttackCommand(IAttackable target)
    {
        Target = target;
    }

    #endregion

}