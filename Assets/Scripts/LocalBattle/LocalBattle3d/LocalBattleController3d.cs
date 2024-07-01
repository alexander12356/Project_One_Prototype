using System.Collections;
using Mech.Data.Global;
using Mech.Data.Local;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleController3d : MonoBehaviour
	{
		public static LocalBattleController3d Instance;

		public ArmyObject PlayerArmyObject;
		public ArmyObject EnemyArmyObject;
		public FightController FightController;
		public GloryPointsGlobalDataList GloryPointsGlobalDataList;

		private ArmyLocalData _playerArmyLocalData;
		private int _possibleGloryPoints;

		private void Awake()
		{
			Instance = this;
		}

		public void Init(ArmyLocalData playerLocalArmy, ArmyLocalData enemyLocalArmy)
		{
			CreateArmy(playerLocalArmy, PlayerArmyObject);
			CreateArmy(enemyLocalArmy, EnemyArmyObject);
			_possibleGloryPoints = CalculateGloryPoints(enemyLocalArmy);

			_playerArmyLocalData = playerLocalArmy;
			LocalBattleUi.Instance.ShowChooseTacticWindow();
			return;

			void CreateArmy(ArmyLocalData armyLocalData, ArmyObject armyObject)
			{
				armyObject.CreateSquads(armyLocalData);
			}

			int CalculateGloryPoints(ArmyLocalData armyLocalData)
			{
				var result = 0;
				foreach (var squadLocalData in armyLocalData.SquadLocalDataList)
				{
					foreach (var modelLocalData in squadLocalData.ModelLocalDataList)
					{
						result += GloryPointsGlobalDataList.GetGloryPoints(modelLocalData.Type);
					}
				}

				return result;
			}
		}

		public void StartBattle()
		{
			StartCoroutine(StartBattleCoroutine());

			IEnumerator StartBattleCoroutine()
			{
				yield return FightController.StartBattleCoroutine(PlayerArmyObject, EnemyArmyObject);

				UpdatePlayerLocalData();

				if (FightController.IsWin())
				{
					LocalBattleUi.Instance.ShowWinWindow(_possibleGloryPoints);
				}
				else
				{
					LocalBattleUi.Instance.ShowLoseWindow();
				}

				PlayerData.Instance.GloryPoints += _possibleGloryPoints;
			}
		}

		private void UpdatePlayerLocalData()
		{
			for (var i = 0; i < _playerArmyLocalData.SquadLocalDataList.Count; i++)
			{
				var squadLocalData = _playerArmyLocalData.SquadLocalDataList[i];
				for (var j = squadLocalData.GetModelCount() - 1; j >= 0; j--)
				{
					var playerModelLocalData = squadLocalData.ModelLocalDataList[j];
					if (!PlayerArmyObject.SquadObjectList[i].ModelList.Exists(x => x.Guid == playerModelLocalData.Guid))
					{
						squadLocalData.ModelLocalDataList.RemoveAt(j);
					}
				}
			}
		}

		public void Clear()
		{
			PlayerArmyObject.Clear();
			EnemyArmyObject.Clear();
		}
	}
}