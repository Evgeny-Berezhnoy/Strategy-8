using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy Game/" + nameof(SelectableValue), order = 0)]
public class SelectableValue : ScriptableObject
{

    #region Fields

    public Action<ISelectable> OnSelected;
    
    #endregion

    #region Properties

    public ISelectable CurrentValue { get; private set; }

    #endregion

    #region Methods

    public void SetValue(ISelectable value)
    {
        
        CurrentValue = value;
        
        OnSelected?.Invoke(value);
    
    }

    #endregion


}