using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = nameof(MechBalance), menuName = "Data/MechBalance")]
	public class MechBalance : ScriptableObject
	{
		public List<BalanceController.SquadGlobalData> Balances;
	}
}