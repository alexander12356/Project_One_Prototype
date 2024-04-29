using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

public class WorldUi : MonoBehaviour, IWorldUi
{
	public GameObject _pauseUi;
	public Toggle _pauseToggle;
	public Toggle _resumeToggle;

	private void Awake()
	{
		EventBus.Subscribe(this);
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(this);
	}

	public void OpenSquadManagementWindow()
	{
		GameController.Instance.OpenSquadManagementWindow();
	}
	
	public void SetPause(bool value)
	{
		_pauseUi.gameObject.SetActive(value);
		if (!value)
		{
			_resumeToggle.SetIsOnWithoutNotify(true);
		}
		else
		{
			_pauseToggle.SetIsOnWithoutNotify(true);
		}
	}

	public void HandlePause(bool value)
	{
		EventBus.RaiseEvent<IGameController>(x => x.SetPause(value));
		_pauseUi.gameObject.SetActive(value);
	}
}

public interface IWorldUi : IGlobalSubscriber
{
	void SetPause(bool value);
}