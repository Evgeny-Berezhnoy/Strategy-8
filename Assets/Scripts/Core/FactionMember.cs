using UnityEngine;
using Zenject;

public class FactionMember : MonoBehaviour, IFactionMember
{

    #region Fields

    [Inject] private IFactionCounter _factionCounterModel;

    [SerializeField] private int _factionID;

    #endregion

    #region Interfaces properties

    public int FactionID => _factionID;

    #endregion

    #region Interfaces methods

    public void SetFaction(int factionID)
    {
        _factionID = factionID;

        _factionCounterModel.Remove(this, true);
        _factionCounterModel.Add(this);
    }

    #endregion

    #region Unity events

    private void Start()
    {
        _factionCounterModel.Add(this);
    }

    private void OnDestroy()
    {
        _factionCounterModel?.Remove(this);
    }

    #endregion

}