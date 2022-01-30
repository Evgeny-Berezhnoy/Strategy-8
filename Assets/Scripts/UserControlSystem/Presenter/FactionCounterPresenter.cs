using UnityEngine;
using Zenject;

[RequireComponent(typeof(FactionCounterView))]
public class FactionCounterPresenter : MonoBehaviour
{

    #region Fields

    [Inject] private IFactionCounter _model;

    private FactionCounterView _view;

    #endregion

    #region Unity events

    private void Awake()
    {
        _view = GetComponent<FactionCounterView>();
        _view.Switch(false);

        _model.OnFactionWon += _view.DeclareFactionVictory;
    }

    #endregion

}