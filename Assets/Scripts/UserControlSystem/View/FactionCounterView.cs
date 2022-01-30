using UnityEngine;
using TMPro;

public class FactionCounterView : MonoBehaviour
{

    #region Fields

    [SerializeField] private TextMeshProUGUI _textHolder;

    #endregion

    #region Methods

    public void DeclareFactionVictory(int factionID)
    {
        Switch(true);

        _textHolder.text = $"Faction {factionID} has won!";
    }

    public void Switch(bool active)
    {
        gameObject.SetActive(active);
    }

    #endregion

}