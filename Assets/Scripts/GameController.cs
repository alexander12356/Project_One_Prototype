using System;
using System.Collections;
using System.Globalization;
using EventBusSystem;
using LocalBattle3d;
using Mech.Data.Local;
using Mech.UI;
using Mech.World;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour, IGameController
{
	[FormerlySerializedAs("IsSquadsStops")] public bool IsPause = false;
	public bool IsWorldControl = false;
	public string StartTime;
	public string TimerDeltaTime;
	public float TimerEndValue;
	public bool IsNight;
	public GameObject WorldCamera;
	public GameObject BattleCamera;

	public static GameController Instance;

	private EnemyArmy _battleArmy;
	public DateTime CurrentDateTime;
	private TimeSpan _deltaTimeSpan;
	private float _timer;
	private int _prevEatedDay;

	private void Awake()
	{
		Instance = this;
		EventBus.Subscribe(this);
	}

	private void Start()
	{
		CurrentDateTime = DateTime.ParseExact(StartTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
		_deltaTimeSpan = TimeSpan.Parse(TimerDeltaTime);
		_prevEatedDay = CurrentDateTime.Day;
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(this);
	}

	private void Update()
	{
		if (IsPause)
		{
			return;
		}

		_timer += Time.deltaTime;

		if (_timer >= TimerEndValue)
		{
			_timer = 0f;
		}
		else
		{
			var coef = Time.deltaTime / TimerEndValue;
			var seconds = _deltaTimeSpan.TotalSeconds * coef;
			CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromSeconds(seconds));
			EventBus.RaiseEvent<IWorldUi>(x => x.SetDateTime(CurrentDateTime));
		}

		if (CurrentDateTime.Hour >= 21)
		{
			EventBus.RaiseEvent<IWorldUi>(x => x.SetNight(true));
			IsNight = true;
		}

		if (CurrentDateTime.Hour is >= 9 and < 21)
		{
			EventBus.RaiseEvent<IWorldUi>(x => x.SetNight(false));
			IsNight = false;
		}

		if (_prevEatedDay != CurrentDateTime.Day)
		{
			Player.Instance.Eat();
			Player.Instance.Pay();
			_prevEatedDay = CurrentDateTime.Day;
		}
	}

	public void StartDialog(GameObject squad)
	{
		IsWorldControl = false;
		IsPause = true;
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(true));

		_battleArmy = squad.GetComponent<EnemyArmy>();
		var dialogueType = _battleArmy.GetDialogueType();
		DialogueWindow.Instance.Init(dialogueType);
		DialogueWindow.Instance.StartDialogue();
	}

	public void StartLocalBattle()
	{
		IsWorldControl = false;

		IsPause = true;
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(true));

		WorldCamera.gameObject.SetActive(false);
		BattleCamera.gameObject.SetActive(true);
		WindowController.Instance.SetLocalBattleUi();
		LocalBattleController3d.Instance.Init(PlayerData.Instance.ArmyLocalData, _battleArmy.GetLocalArmyData);
	}

	public void CompleteLocalBattle()
	{
		IsWorldControl = true;
		Destroy(_battleArmy.gameObject);
		_battleArmy = null;
		WindowController.Instance.SetWorldUi();
		WorldCamera.gameObject.SetActive(true);
		BattleCamera.gameObject.SetActive(false);
		Player.Instance.UpdateView();

		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(false));
		IsPause = false;
	}

	public void OpenSquadManagementWindow()
	{
		IsWorldControl = false;
		ArmyManagementWindow.Instance.Open();
	}

	public void ReturnFromSquadManagementWindow()
	{
		IsWorldControl = true;
	}

	public void SetPause(bool value)
	{
		IsPause = value;
	}

	public void StartCity(GameObject cityGameObject)
	{
		IsWorldControl = false;
		IsPause = true;
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(true));

		var city = cityGameObject.GetComponent<City>();
		city.Visit();
	}

	public void CloseCity()
	{
		IsWorldControl = true;
	}

	public void OpenFocusWindow()
	{
		IsWorldControl = false;
		FocusesWindow.Instance.Open();
	}
}

public interface IGameController : IGlobalSubscriber
{
	void SetPause(bool value);
	void CloseCity();
}