public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
{

    #region Base Methods

    protected override IAttackCommand CreateCommand(IAttackable argument)
    {
        return new AttackCommand(argument);
    }

    #endregion

}