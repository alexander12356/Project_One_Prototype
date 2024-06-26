﻿using System;
using System.Collections.Generic;
using Mech.Data.Global;
using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleController3d : MonoBehaviour
	{
		public ArmyObject PlayerArmyObject;
		public ArmyObject EnemyArmyObject;
		public FightController FightController;

		public void Init(ArmyGlobalData playerGlobalArmy, ArmyGlobalData enemyGlobalArmy)
		{
			CreateArmy(playerGlobalArmy, PlayerArmyObject);
			CreateArmy(enemyGlobalArmy, EnemyArmyObject);
		}

		public void StartBattle()
		{
			StartCoroutine(FightController.StartBattleCoroutine(PlayerArmyObject, EnemyArmyObject));
		}

		private void CreateArmy(ArmyGlobalData armyGlobalData, ArmyObject armyObject)
		{
			armyObject.CreateSquads(armyGlobalData);
		}
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