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
		public FreeFlyCamera FreeFlyCamera;

		public IEnumerator StartBattleCoroutine(ArmyObject playerArmy, ArmyObject enemyArmy)
		{
			FreeFlyCamera.Activate(true);
			while (!IsFightEnd())
			{
				yield return new WaitForSeconds(BeforeTurnDelay);

				var playerSquadsTargets = GetSquadTargetList(playerArmy, enemyArmy);
				var enemySquadsTargets = GetSquadTargetList(enemyArmy, playerArmy);
				SquadAttack(playerSquadsTargets, playerArmy.SquadObjectList, enemyArmy.SquadObjectList);
				SquadAttack(enemySquadsTargets, enemyArmy.SquadObjectList, playerArmy.SquadObjectList);

				RemoveDeadModels(playerArmy);
				RemoveDeadModels(enemyArmy);

				yield return new WaitForSeconds(AnimationDelay);
			}

			yield return new WaitForSeconds(BeforeResultDelay);

			FreeFlyCamera.Activate(false);
			FreeFlyCamera.ReturnToInitPosition();

			yield break;

			bool IsArmyDestroyed(ArmyObject armyObject)
			{
				foreach (var squadObject in armyObject.SquadObjectList)
				{
					foreach (var modelObject in squadObject.ModelList)
					{
						if (!modelObject.IsDead())
						{
							return false;
						}
					}
				}

				return true;
			}

			bool IsFightEnd()
			{
				var isPlayerArmyDestroyed = IsArmyDestroyed(playerArmy);
				var isEnemyArmyDestroyed = IsArmyDestroyed(enemyArmy);
				return isPlayerArmyDestroyed || isEnemyArmyDestroyed;
			}

			void RemoveDeadModels(ArmyObject armyObject)
			{
				for (var i = 0; i < armyObject.SquadObjectList.Count; i++)
				{
					var squadObject = armyObject.SquadObjectList[i];
					for (var j = squadObject.ModelList.Count - 1; j >= 0; j--)
					{
						if (squadObject.ModelList[j].IsDead())
						{
							Destroy(squadObject.ModelList[j].gameObject);
							squadObject.ModelList.RemoveAt(j);
						}
					}
				}
			}
		}

		private List<(int, int)> GetSquadTargetList(ArmyObject attackerArmy, ArmyObject defenderArmy)
		{
			var result = new List<(int, int)>();
			for (var i = 0; i < attackerArmy.SquadObjectList.Count; i++)
			{
				if (IsSquadEmpty(attackerArmy.SquadObjectList[i]))
				{
					continue;
				}

				if (IsSquadEmpty(defenderArmy.SquadObjectList[i]))
				{
					for (var j = 0; j < defenderArmy.SquadObjectList.Count; j++)
					{
						if (!IsSquadEmpty(defenderArmy.SquadObjectList[j]))
						{
							result.Add((i, j));
							break;
						}
					}
				}
				else
				{
					result.Add((i, i));
				}
			}

			return result;

			bool IsSquadEmpty(SquadObject squadObject)
			{
				return squadObject.ModelList.Count == 0;
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
				attackerModelList[target.Item1].Attack(defenderModelList[target.Item2]);
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