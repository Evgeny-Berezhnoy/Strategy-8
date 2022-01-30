using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(OutlinableValue), menuName = "Strategy Game/" + nameof(OutlinableValue), order = 1)]
public class OutlinableValue : ScriptableObject
{

    #region Properties

    private List<IOutlinable> _currentValues = new List<IOutlinable>();

    #endregion

    #region Methods

    public void SetValue(IOutlinable value)
    {

        if (value == null)
        {

            ToggleCurrentValues(false);

            _currentValues = null;

            return;

        };

        var values = new List<IOutlinable>();
        
        values.Add(value);

        SetValues(values);

    }

    public void SetValues(List<IOutlinable> values)
    {

        ToggleCurrentValues(false);

        _currentValues = values;

        ToggleCurrentValues(true);

    }

    private void ToggleCurrentValues(bool enabled)
    {
        if (_currentValues != null)
        {
            for (int i = 0; i < _currentValues.Count; i++)
            {
                var currentValue = _currentValues[i]?.OutlineDraw;

                if(currentValue != null)
                {
                    currentValue.enabled = enabled;
                };
            };
        };
    }

    #endregion

}