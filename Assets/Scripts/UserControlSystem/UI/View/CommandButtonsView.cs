using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonsView : MonoBehaviour
{

	#region Fields
		
	[SerializeField] private Button _attackButton;
	[SerializeField] private Button _moveButton;
	[SerializeField] private Button _patrolButton;
	[SerializeField] private Button _stopButton;
	[SerializeField] private Button _produceUnitButton;

	public Action<ICommandExecutor> OnClick;
	
	private Dictionary<Type, Button> _buttonsByExecutorType;

    #endregion

    #region Unity Events

    private void Start()
    {

		_buttonsByExecutorType = new Dictionary<Type, Button>();
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);

	}

    #endregion

    #region Methods

	public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
    {

		foreach(var currentExecutor in commandExecutors)
        {

			var buttonCollocations =
				_buttonsByExecutorType
				.Where(type => type.Key.IsAssignableFrom(currentExecutor.GetType()));

			foreach(var buttonCollocation in buttonCollocations)
            {

				var button = buttonCollocation.Value;

				button.gameObject.SetActive(true);
				button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
			
			};

        };

    }

	public void Clear()
    {

		foreach(var keyValuePair in _buttonsByExecutorType)
        {

			keyValuePair.Value.onClick.RemoveAllListeners();
			keyValuePair.Value.gameObject.SetActive(false);

		}

    }

    #endregion

}
