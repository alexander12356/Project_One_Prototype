using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using UnityEngine;

namespace DefaultNamespace
{
	public partial class BalanceController : MonoBehaviour
	{
		[Serializable]
		public struct GettedExp
		{
			public ModelType ModelType;
			public string Id;
			public int Exp;
		}
		
		[Serializable]
		public struct LevelUp
		{
			public string Id;
			public int Exp;
		}

		public ModelGlobalDataList Balances;
		public List<GettedExp> GettedExps;
		public List<LevelUp> LevelUps;

		public static BalanceController Instance;

		private void Awake()
		{
			Instance = this;
		}

		public ModelGlobalData GetSquadGlobalData(string id)
		{
			return Balances.Balances.FirstOrDefault(x => x.Id == id);
		}

		public int GetExpFrom(string id)
		{
			return GettedExps.FirstOrDefault(x => x.Id == id).Exp;
		}

		public int ExpForLevelUp(string id)
		{
			return LevelUps.FirstOrDefault(x => x.Id == id).Exp;
		}

		public int GetNeedMoneys(string squadId)
		{
			return Balances.Balances.FirstOrDefault(x => x.Id == squadId).NeedMoney;
		}
	}
}