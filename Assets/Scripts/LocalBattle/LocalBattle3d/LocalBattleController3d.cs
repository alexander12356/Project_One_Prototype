using System.Collections;
using Mech.Data.LocalData;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleController3d : MonoBehaviour
	{
		public static LocalBattleController3d Instance;

		public ArmyObject PlayerArmyObject;
		public ArmyObject EnemyArmyObject;
		public FightController FightController;

		private void Awake()
		{
			Instance = this;
		}

		public void Init(ArmyLocalData playerLocalArmy, ArmyLocalData enemyLocalArmy)
		{
			CreateArmy(playerLocalArmy, PlayerArmyObject);
			CreateArmy(enemyLocalArmy, EnemyArmyObject);

			void CreateArmy(ArmyLocalData armyLocalData, ArmyObject armyObject)
			{
				armyObject.CreateSquads(armyLocalData);
			}

			LocalBattleUi.Instance.ShowChooseTacticWindow();
		}

		public void StartBattle()
		{
			StartCoroutine(StartBattleCoroutine());

			IEnumerator StartBattleCoroutine()
			{
				yield return FightController.StartBattleCoroutine(PlayerArmyObject, EnemyArmyObject);

				if (FightController.IsWin())
				{
					LocalBattleUi.Instance.ShowWinWindow();
				}
				else
				{
					LocalBattleUi.Instance.ShowLoseWindow();
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