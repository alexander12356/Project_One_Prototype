using System;
using System.Collections;
using EventBusSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameController
{
	public bool IsSquadsStops = false;
	public bool IsWorldControl = false;

	public static GameController Instance;

	private EnemySquad BattleSquad;

	private void Awake()
	{
		Instance = this;
		EventBus.Subscribe(this);
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(this);
	}

	public void StartLocalBattle(GameObject squad)
	{
		IsWorldControl = false;
		BattleSquad = squad.GetComponent<EnemySquad>();
		IsSquadsStops = true;
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
		EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(false));
		Destroy(BattleSquad.gameObject);
		BattleSquad = null;
		IsSquadsStops = false;
		SceneManager.UnloadSceneAsync("LocalBattle");
		WindowController.Instance.SetWorldUi();
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
		IsSquadsStops = value;
	}
}

public interface IGameController : IGlobalSubscriber
{
	void SetPause(bool value);
}