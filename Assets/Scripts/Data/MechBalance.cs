using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using LocalBattle3d;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = nameof(MechBalance), menuName = "Data/MechBalance")]
	public class MechBalance : ScriptableObject
	{
		public List<BalanceController.SquadGlobalData> Balances;
		public List<BalanceController.GettedExp> GettedExps;

		public BalanceController.SquadGlobalData GetMechBalance(ModelType type)
		{
			return Balances.FirstOrDefault(x => x.ModelType == type);
		}

		public int GetExpFrom(ModelType type)
		{
			return GettedExps.FirstOrDefault(x => x.ModelType == type).Exp;
		}
	}
}