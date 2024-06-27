using System.Collections.Generic;
using DefaultNamespace;
using LocalBattle3d;
using UnityEngine;

public class LocalBattleUi : MonoBehaviour
{
	public static LocalBattleUi Instance;
	public SquadLevelUp SquadLevelUpPrefab;
	public Transform LevelUpHolder;
	public GameObject WinUi;
	public GameObject LoseUi;
	public GameObject ChooseTacticWindow;

	private void Awake()
	{
		Instance = this;
	}

	public void ShowChooseTacticWindow()
	{
		ChooseTacticWindow.gameObject.SetActive(true);
	}

	public void ShowWinWindow()
	{
		WinUi.SetActive(true);
		/*
		foreach (var squadObject in playerSquadObjects)
		{
			var levelUp = Instantiate(SquadLevelUpPrefab, LevelUpHolder);
			var maxExp = BalanceController.Instance.ExpForLevelUp(squadObject.SquadLocalData.Id);
			levelUp.SetExp(squadObject.SquadLocalData.Exp, squadObject.GettedExp, maxExp);
			levelUp.SetIcon(BalanceController.Instance.GetSquadGlobalData(squadObject.SquadLocalData.Id).Icon);
			squadObject.SquadLocalData.Exp += squadObject.GettedExp;
			if (squadObject.SquadLocalData.Exp >= maxExp)
			{
				levelUp.SetLevelUp(true);
				squadObject.SquadLocalData.IsLevelUp = true;
			}
		}
		*/
	}

	public void ShowLoseWindow()
	{
		LoseUi.SetActive(true);
	}

	public void Close()
	{
		for (int i = 0; i < LevelUpHolder.childCount; i++)
		{
			var child = LevelUpHolder.GetChild(i);
			Destroy(child.gameObject);
		}
		WinUi.SetActive(false);
		LoseUi.SetActive(false);

		GameController.Instance.CompleteLocalBattle();
		LocalBattleController3d.Instance.Clear();
	}
}