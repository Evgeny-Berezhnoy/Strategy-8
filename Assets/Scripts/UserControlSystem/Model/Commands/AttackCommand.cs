public class AttackCommand : IAttackCommand
{

    #region Interface Properties

    public IPointable Target { get; private set; }

    #endregion

    #region Constructors

    public AttackCommand(IPointable target)
    {

        Target = target;

    }

    #endregion

}