using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class FactionCounterModel : IFactionCounter
{

    #region Interfaces events

    public event Action<int> OnFactionWon;

    #endregion

    #region Fields

    private ReactiveDictionary<int, HashSet<IFactionMember>> _factions;
    private IDisposable _removeSubscribtion;

    #endregion

    #region Constructors

    public FactionCounterModel()
    {
        _factions           = new ReactiveDictionary<int, HashSet<IFactionMember>>();
        
        _removeSubscribtion = _factions.ObserveRemove().Subscribe(_ => CheckVictoryConditions());
    }

    #endregion

    #region Destructors

    ~FactionCounterModel()
    {
        _removeSubscribtion.Dispose();

        var subscribtions = 
            OnFactionWon
                .GetInvocationList()
                .ToList()
                .Cast<Action<int>>()
                .ToList();

        for(int i = 0; i < subscribtions.Count; i++)
        {
            OnFactionWon -= subscribtions[i];
        };
    }

    #endregion

    #region Interfaces methods

    public void Add(IFactionMember factionMember)
    {
        if (!_factions.ContainsKey(factionMember.FactionID))
        {
            _factions.Add(factionMember.FactionID, new HashSet<IFactionMember>());
        };

        var factionMembers = _factions[factionMember.FactionID];

        factionMembers.Add(factionMember);
    }

    public void Remove(IFactionMember factionMember, bool silentRemove = false)
    {
        if (silentRemove)
        {
            Remove(factionMember);

            return;
        };

        if (!_factions.TryGetValue(factionMember.FactionID, out var factionMembers)) return;

        factionMembers.Remove(factionMember);

        if (silentRemove) return;

        if (factionMembers.Count == 0)
        {
            _factions.Remove(factionMember.FactionID);
        };
    }

    #endregion

    #region Methods

    private void CheckVictoryConditions()
    {
        if(_factions.Count == 1)
        {
            foreach(var factionData in _factions)
            {
                OnFactionWon?.Invoke(factionData.Key);
            };
        };
    }

    #endregion

}