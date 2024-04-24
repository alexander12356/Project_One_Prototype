using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
	public bool IsSquadsStops = false;
	public bool IsWorldControl = false;

	public static GameController Instance;

	private GameObject BattleSquad;

	private void Awake()
	{
		Instance = this;
	}

	public void StartLocalBattle(GameObject squad)
	{
		IsWorldControl = false;
		BattleSquad = squad;
		IsSquadsStops = true;
		SceneManager.LoadSceneAsync("LocalBattle", LoadSceneMode.Additive);
		WindowController.Instance.SetLocalBattleUi();
	}

	public void CompleteLocalBattle()
	{
		IsWorldControl = true;
		Destroy(BattleSquad);
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