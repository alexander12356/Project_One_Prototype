using System;
using System.Collections.Generic;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleController3d : MonoBehaviour
	{
		public ArmyObject PlayerArmyObject;
		public ArmyObject EnemyArmyObject;

		public void StartBattle(ArmyGlobalData playerGlobalArmy, ArmyGlobalData enemyGlobalArmy)
		{
			CreateArmy(playerGlobalArmy, PlayerArmyObject);
			CreateArmy(enemyGlobalArmy, EnemyArmyObject);
			/*
			StartCoroutine(StartBattleCoroutine(playerLocalArmy, enemyLocalArmy));

			IEnumerator StartBattleCoroutine(ArmyObject playerArmy, ArmyObject enemyArmy)
			{
				yield return WaitStartFightCoroutine();
				yield return StartFightsCoroutine(playerArmy, enemyArmy);
				CheckResult();
			}
			*/
		}

		private void CreateArmy(ArmyGlobalData armyGlobalData, ArmyObject armyObject)
		{
			armyObject.CreateSquads(armyGlobalData);
		}
	}

	[Serializable]
	public enum ModelType
	{
		None = 0,
		RestoredLineMech = 1,
		BaseLineMech,
		VeteranLineMech,
		BaseHeavySupportMech,
		VeteranHeavySupportMech,
		BaseTerminatorMech,
		VeteranTerminatorMech,
		RestoredChampionMech,
		BaseChampionMech,
		VeteranChampionMech
	}

	[Serializable]
	public struct ArmyGlobalData
	{
		public List<SquadGlobalData> SquadList;
	}

	[Serializable]
	public struct SquadGlobalData
	{
		public List<ModelType> ModelList;
	}
}