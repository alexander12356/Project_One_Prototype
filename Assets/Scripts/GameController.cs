using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public bool IsSquadsStops = false;

	public static GameController Instance;

	private GameObject BattleSquad;

	private void Awake()
	{
		Instance = this;
	}

	public void StartLocalBattle(GameObject squad)
	{
		BattleSquad = squad;
		IsSquadsStops = true;
		SceneManager.LoadSceneAsync("LocalBattle", LoadSceneMode.Additive);
		WindowController.Instance.SetLocalBattleUi();
	}

	public void CompleteLocalBattle()
	{
		Destroy(BattleSquad);
		BattleSquad = null;
		IsSquadsStops = false;
		SceneManager.UnloadSceneAsync("LocalBattle");
		WindowController.Instance.SetWorldUi();
	}
}