using UnityEngine;
using Zenject;

using static AutoAttackEvaluator;

public class FactionMemberParrallelInfoUpdater : MonoBehaviour, ITickable
{
    #region Fields

    private IFactionMember _factionMember;

    #endregion

    #region Unity events

    private void Awake()
    {
        _factionMember = GetComponent<IFactionMember>();
    }

    private void OnDestroy()
    {
        FACTION_MEMBERS_INFO.TryRemove(gameObject, out _);
    }

    #endregion

    #region Interfaces methods

    public void Tick()
    {
        FACTION_MEMBERS_INFO
            .AddOrUpdate(
                gameObject,
                new FactionMemberParallelInfo(transform.position, _factionMember.FactionID),
                (_, value) =>
                {
                    value.Position  = transform.position;
                    value.Faction   = _factionMember.FactionID;
                    return value;
                });
    }

    #endregion
}