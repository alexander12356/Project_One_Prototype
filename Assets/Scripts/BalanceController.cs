using System;
using System.Collections.Generic;
using System.Linq;
using LocalBattle3d;
using UnityEngine;

namespace DefaultNamespace
{
	public class BalanceController : MonoBehaviour
	{
		[Serializable]
		public struct SquadGlobalData
		{
			public ModelType ModelType;
			public Sprite Icon;
			public string Id;
			public int BS;
			public int S;
			public int T;
			public int W;
			public int A;
			public int D;
			public int Sv;
			public int NeedMoney;
		}

		[Serializable]
		public struct GettedExp
		{
			public string Id;
			public int Exp;
		}
		
		[Serializable]
		public struct LevelUp
		{
			public string Id;
			public int Exp;
		}

		public List<SquadGlobalData> Balances;
		public List<GettedExp> GettedExps;
		public List<LevelUp> LevelUps;

		public static BalanceController Instance;

		private void Awake()
		{
			Instance = this;
		}

		public SquadGlobalData GetSquadGlobalData(string id)
		{
			return Balances.FirstOrDefault(x => x.Id == id);
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
			return Balances.FirstOrDefault(x => x.Id == squadId).NeedMoney;
		}
	}
}