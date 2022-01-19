public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IPointable>
{

    #region Base Methods

    protected override IAttackCommand CreateCommand(IPointable argument)
    {
        return new AttackCommand(argument);
    }

    #endregion

}