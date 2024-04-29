using System;
using System.Collections;
using System.Globalization;
using EventBusSystem;
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

	public static GameController Instance;

	private EnemySquad BattleSquad;
	private DateTime _currentDateTime;
	private TimeSpan _deltaTimeSpan;
	private float _timer;

	private void Awake()
	{
		Instance = this;
		EventBus.Subscribe(this);
	}

	private void Start()
	{
		_currentDateTime = DateTime.ParseExact(StartTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
		_deltaTimeSpan = TimeSpan.Parse(TimerDeltaTime);
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
			_currentDateTime = _currentDateTime.Add(TimeSpan.FromSeconds(seconds));
			EventBus.RaiseEvent<IWorldUi>(x => x.SetDateTime(_currentDateTime));
		}

		if (_currentDateTime.Hour >= 21)
		{
			EventBus.RaiseEvent<IWorldUi>(x => x.SetNight(true));
			IsNight = true;
		}

		if (_currentDateTime.Hour is >= 9 and < 21)
		{
			EventBus.RaiseEvent<IWorldUi>(x => x.SetNight(false));
			IsNight = false;
		}
	}

	public void StartLocalBattle(GameObject squad)
	{
		IsWorldControl = false;
		BattleSquad = squad.GetComponent<EnemySquad>();

		IsPause = true;
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(true));

		var op = SceneManager.LoadSceneAsync("LocalBattle", LoadSceneMode.Additive);
		StartCoroutine(ShowLocalBattleCoroutine(op));
	}

	private IEnumerator ShowLocalBattleCoroutine(AsyncOperation operation)
	{
		while (!operation.isDone)
		{
			yield return null;
		}

		WindowController.Instance.SetLocalBattleUi();
		LocalBattleController.Instance.SetEnemySquad(BattleSquad.GetLocalSquadData());
		LocalBattleController.Instance.StartBattle();
	}

	public void CompleteLocalBattle()
	{
		IsWorldControl = true;
		Destroy(BattleSquad.gameObject);
		BattleSquad = null;
		SceneManager.UnloadSceneAsync("LocalBattle");
		WindowController.Instance.SetWorldUi();

		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(false));
		IsPause = false;
	}

	public void OpenSquadManagementWindow()
	{
		IsWorldControl = false;
		SquadManagementWindow.Instance.Open();
	}

	public void ReturnFromSquadManagementWindow()
	{
		IsWorldControl = true;
	}

	public void SetPause(bool value)
	{
		IsPause = value;
	}

	public void StartCity(GameObject city)
	{
		IsWorldControl = false;
		EventBus.RaiseEvent<IWorldUi>(x => x.OpenCityUi(city));

		IsPause = true;
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(true));

		Player.Instance.SetVisible(false);
		Player.Instance.SetPosition(city.GetComponent<City>().EnterPosition);
	}

	public void CloseCity(City city)
	{
		IsWorldControl = true;

		Player.Instance.SetVisible(true);
		Player.Instance.SetPosition(city.ExitPosition);
	}
}

public interface IGameController : IGlobalSubscriber
{
	void SetPause(bool value);
	void CloseCity(City city);
}