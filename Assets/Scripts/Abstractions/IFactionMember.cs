public interface IFactionMember
{

    #region Properties

    int FactionID { get; }

    #endregion

    #region Methods

    void SetFaction(int factionID);

    #endregion

}