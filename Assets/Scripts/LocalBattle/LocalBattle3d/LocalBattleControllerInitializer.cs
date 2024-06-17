using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleControllerInitializer : MonoBehaviour
	{
		public LocalBattleController3d LocalBattleController3d;
		public List<SquadGlobalData> PlayerArmy;
		public List<SquadGlobalData> EnemyArmy;

		[Button]
		public void Init()
		{
			var playerGlobalArmyData = new ArmyGlobalData
			{
				SquadList = PlayerArmy
			};
			var enemyGlobalArmyData = new ArmyGlobalData
			{
				SquadList = EnemyArmy
			};
			LocalBattleController3d.StartBattle(playerGlobalArmyData, enemyGlobalArmyData);
		}
	}
}