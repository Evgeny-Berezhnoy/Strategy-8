using System;

public interface IFactionCounter
{

    #region Events

    event Action<int> OnFactionWon;

    #endregion

    #region Methods

    void Add(IFactionMember factionMember);
    void Remove(IFactionMember factionMember, bool silentRemove = false);

    #endregion

}