using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomLeftPresenter : MonoBehaviour
{

	#region Fields

	[SerializeField] private Image _selectedImage;
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Image _sliderBackground;
	[SerializeField] private Image _sliderFillImage;
	[SerializeField] private SelectableValue _selectedValue;

    #endregion

    #region Unity Events

    private void Start()
	{

		_selectedValue.OnSelected += onSelected;
		
		onSelected(_selectedValue.CurrentValue);
	
	}

	#endregion

	#region Methods

	private void onSelected(ISelectable selected)
	{

		var isSelected = (selected != null);

		_text.enabled			= isSelected;
		_selectedImage.enabled	= isSelected;
		
		_healthSlider.gameObject.SetActive(isSelected);
		
		if (isSelected)
		{
			_selectedImage.sprite	= selected.Icon;
			
			_text.text				= $"{selected.Health}/{selected.MaxHealth}";
			
			_healthSlider.minValue	= 0;
			_healthSlider.maxValue	= selected.MaxHealth;
			_healthSlider.value		= selected.Health;
			
			var color				= Color.Lerp(Color.red, Color.green, selected.Health / (float)selected.MaxHealth);
			
			_sliderBackground.color = color * 0.5f;
			_sliderFillImage.color	= color;
		
		}

	}

	#endregion

}