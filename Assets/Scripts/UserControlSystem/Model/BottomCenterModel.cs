using System;
using System.Linq;
using UnityEngine;
using UniRx;
using Zenject;

public class BottomCenterModel
{

    #region Properties

    public IObservable<IUnitProducer> UnitProducers { get; private set; }

    #endregion

    #region Methods

    [Inject]
    public void Init(IObservable<ISelectable> currentlySelected)
    {
        UnitProducers =
            currentlySelected
            .Select(selectable => selectable as Component)
            .Select(component => component?.GetComponent<IUnitProducer>());
    }

    #endregion

}