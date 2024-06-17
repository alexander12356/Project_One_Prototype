using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalBattle3d
{
	public class FightController : MonoBehaviour
	{
		public float BeforeTurnDelay;
		public float AnimationDelay;
		public float BeforeResultDelay;

		private IEnumerator StartBattleCoroutine(ArmyObject playerArmy, ArmyObject enemyArmy)
		{
			var isBattleEnd = false;

			/*
			while (!IsFightEnd())
			{
				var playerSquadsTargets = GetSquadTargetList(playerArmy, enemyArmy);
				var enemySquadsTargets = GetSquadTargetList(enemyArmy, playerArmy);
				SquadAttack(playerSquadsTargets, playerArmy, enemyArmy);
				SquadAttack(enemySquadsTargets, enemyArmy, playerArmy);
			}

			yield return ShowBattleEndWindow();
			*/
			yield return null;
		}

		private List<(int, int)> GetSquadTargetList(ArmyObject attackerArmy, ArmyObject defenderArmy)
		{
			var attackerSquadsCount = GetArmySquads(attackerArmy);
			var defenderSquadsCount = GetArmySquads(defenderArmy);
			return GetTargets(attackerSquadsCount, defenderSquadsCount);

			int GetArmySquads(ArmyObject armyObject)
			{
				for (var i = 0; i < armyObject.SquadObjectList.Count; i++)
				{
					if (armyObject.SquadObjectList[i].ModelList.Count == 0)
					{
						return i;
					}
				}

				return 0;
			}
		}

		private void SquadAttack(List<(int, int)> squadsTargets, List<SquadObject> attackerSquadList, List<SquadObject> defenderSquadList)
		{
			foreach (var squadsTarget in squadsTargets)
			{
				var attackerModelsCount = attackerSquadList[squadsTarget.Item1].ModelList.Count;
				var defenderModelsCount = defenderSquadList[squadsTarget.Item2].ModelList.Count;
				var targets = GetTargets(attackerModelsCount, defenderModelsCount);
				ModelsAttack(targets, attackerSquadList[squadsTarget.Item1].ModelList, defenderSquadList[squadsTarget.Item2].ModelList);
			}
		}

		private void ModelsAttack(List<(int, int)> targets, List<ModelObject> attackerModelList, List<ModelObject> defenderModelList)
		{
			foreach (var target in targets)
			{
				//attackerModelList[target.Item1].Attack(defenderModelList[target.Item2]);
			}
		}

		private List<(int, int)> GetTargets(int count1, int count2)
		{
			var targets = new List<(int, int)>();
			int j = 0;
			for (int i = 0; i < count1; i++)
			{
				if (i < count2)
				{
					targets.Add((i, i));
				}
				else
				{
					if (j >= count2)
					{
						j = 0;
					}

					targets.Add((i, j));
					j++;
				}
			}

			return targets;
		}
	}
}