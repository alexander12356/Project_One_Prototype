using System.Collections.Generic;
using UnityEngine;

namespace LocalBattle3d
{
	public class ArmyObject : MonoBehaviour
	{
		public List<SquadObject> SquadObjectList;

		public void CreateSquads(ArmyGlobalData armyGlobalData)
		{
			for (int i = 0; i < armyGlobalData.SquadList.Count; i++)
			{
				SquadObjectList[i].CreateModels(armyGlobalData.SquadList[i]);
			}
		}
	}
}