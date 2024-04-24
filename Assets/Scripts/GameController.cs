using UnityEngine;

public class GameController : MonoBehaviour
{
	public bool IsSquadsStops = false;

	public static GameController Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void StartLocalBattle()
	{
		IsSquadsStops = true;
	}

	public void CompleteLocalBattle()
	{
		IsSquadsStops = false;
	}
}