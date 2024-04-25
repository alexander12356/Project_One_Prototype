using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
	public bool IsSquadsStops = false;
	public bool IsWorldControl = false;

	public static GameController Instance;

	private EnemySquad BattleSquad;

	private void Awake()
	{
		Instance = this;
	}

	public void StartLocalBattle(GameObject squad)
	{
		IsWorldControl = false;
		BattleSquad = squad.GetComponent<EnemySquad>();
		IsSquadsStops = true;
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
	}

	public void CompleteLocalBattle()
	{
		IsWorldControl = true;
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
}