using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public bool IsSquadsStops = false;
	public bool IsLocalBattle = false;

	public static GameController Instance;

	private GameObject BattleSquad;

	private void Awake()
	{
		Instance = this;
	}

	public void StartLocalBattle(GameObject squad)
	{
		IsLocalBattle = true;
		BattleSquad = squad;
		IsSquadsStops = true;
		SceneManager.LoadSceneAsync("LocalBattle", LoadSceneMode.Additive);
		WindowController.Instance.SetLocalBattleUi();
	}

	public void CompleteLocalBattle()
	{
		IsLocalBattle = false;
		Destroy(BattleSquad);
		BattleSquad = null;
		IsSquadsStops = false;
		SceneManager.UnloadSceneAsync("LocalBattle");
		WindowController.Instance.SetWorldUi();
	}
}