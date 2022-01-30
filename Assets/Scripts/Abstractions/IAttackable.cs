public interface IAttackable : IHealthHolder
{

    #region Methods

    void RecieveDamage(int amount);

    #endregion

}