using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
	public class BalanceController : MonoBehaviour
	{
		[Serializable]
		public struct SquadGlobalData
		{
			public Sprite Icon;
			public string Id;
			public int BS;
			public int S;
			public int T;
			public int W;
			public int A;
			public int D;
			public int Sv;
		}

		public List<SquadGlobalData> Balances;

		public static BalanceController Instance;

		private void Awake()
		{
			Instance = this;
		}

		public SquadGlobalData GetSquadGlobalData(string id)
		{
			return Balances.FirstOrDefault(x => x.Id == id);
		}
	}
}