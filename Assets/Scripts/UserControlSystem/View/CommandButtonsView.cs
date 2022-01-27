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
	[SerializeField] private Button _rendezvousPointButton;

	public Action<ICommandExecutor, ICommandQueueManager> OnClick;
	
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
		_buttonsByExecutorType.Add(typeof(CommandExecutorBase<IRendezvousPointCommand>), _rendezvousPointButton);

	}

    #endregion

    #region Methods

	public void BlockInteractions(ICommandExecutor commandExecutor)
    {

		UnblockAllInteractions();

		GetButtonGameobjectByType(commandExecutor.GetType()).GetComponent<Selectable>().interactable = false;

    }

	public void UnblockAllInteractions()
    {

		SetInteractable(true);

    }

	public void SetInteractable(bool value)
    {

		_attackButton
			.GetComponent<Selectable>().interactable = value;
		
		_moveButton
			.GetComponent<Selectable>().interactable = value;
		
		_patrolButton
			.GetComponent<Selectable>().interactable = value;
		
		_stopButton
			.GetComponent<Selectable>().interactable = value;
		
		_produceUnitButton
			.GetComponent<Selectable>().interactable = value;

		_rendezvousPointButton
			.GetComponent<Selectable>().interactable = value;

	}

	public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors, ICommandQueueManager commandQueueManager)
    {

		foreach(var currentExecutor in commandExecutors)
        {

			var button = GetButtonByType(currentExecutor.GetType());
			
			button.gameObject.SetActive(true);
			button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, commandQueueManager));

        };

    }

	private GameObject GetButtonGameobjectByType(Type executorInstanceType)
    {

		return GetButtonByType(executorInstanceType).gameObject;

	}

	private Button GetButtonByType(Type executorInstanceType)
    {

		return
			_buttonsByExecutorType
				.Where(type => type.Key.IsAssignableFrom(executorInstanceType))
				.First()
				.Value;

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