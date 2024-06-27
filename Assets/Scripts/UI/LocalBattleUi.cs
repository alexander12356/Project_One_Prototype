using System.Collections.Generic;
using DefaultNamespace;
using LocalBattle3d;
using Mech.Data.Global;
using Mech.Data.LocalData;
using UnityEngine;

public class LocalBattleUi : MonoBehaviour
{
	public class ModelStat
	{
		public int Count;
		public int LevelUpCount;
	}

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

		var playerArmyLocalData = PlayerData.Instance.ArmyLocalData;
		var modelStatList = new Dictionary<ModelType, ModelStat>();
		foreach (var squadLocalData in playerArmyLocalData.SquadLocalDataList)
		{
			foreach (var modelLocalData in squadLocalData.ModelLocalDataList)
			{
				var modelType = modelLocalData.Type;
				var isLevelUp = modelLocalData.IsLevelUp;
				if (modelStatList.ContainsKey(modelType))
				{
					modelStatList[modelType].Count++;
					if (isLevelUp)
					{
						modelStatList[modelType].LevelUpCount++;
					}
					continue;
				}
				modelStatList.Add(modelType, new ModelStat
				{
					Count = 1,
					LevelUpCount = isLevelUp ? 1 : 0
				});
			}
		}

		foreach (var modelStat in modelStatList)
		{
			var levelUp = Instantiate(SquadLevelUpPrefab, LevelUpHolder);
			levelUp.SetStat(modelStat);
		}
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