using UnityEngine;

public interface ISelectable
{

    #region Properties

    float Health { get; }
    float MaxHealth { get; }
    Sprite Icon { get; }
    
    #endregion

}