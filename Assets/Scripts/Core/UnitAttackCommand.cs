using UnityEngine;

public class UnitAttackCommand : CommandExecutorBase<IAttackCommand>
{
    
    #region Base Methods

    public override void ExecuteSpecificCommand(IAttackCommand command)
    {

        Debug.Log("Attack command has been executed.");

    }

    #endregion

}