using System.Collections.Generic;
using Mech.Data.Global;
using Mech.Data.LocalData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleControllerInitializer : MonoBehaviour
	{
		public LocalBattleController3d LocalBattleController3d;
		public int SquadCount;
		public int ModelCount;
		public ArmyLocalData PlayerArmy;
		public ArmyLocalData EnemyArmy;

		[Button]
		public void Init()
		{
			for (int i = 0; i < SquadCount; i++)
			{
				for (int j = 0; j < ModelCount; j++)
				{
					PlayerArmy.AddModel(i, ModelType.RestoredLineMech);
					EnemyArmy.AddModel(i, ModelType.RestoredLineMech);
				}
			}
			LocalBattleController3d.Init(PlayerArmy, EnemyArmy);
		}
	}
}