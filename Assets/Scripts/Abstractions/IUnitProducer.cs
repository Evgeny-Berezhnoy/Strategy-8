using UnityEngine;
using UniRx;

public interface IUnitProducer
{

    #region Properties

    IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
    Transform PivotPoint { get; }
    Transform InstantiationPoint { get; }

    #endregion

    #region Methods

    void Cancel(int index);

    #endregion

}