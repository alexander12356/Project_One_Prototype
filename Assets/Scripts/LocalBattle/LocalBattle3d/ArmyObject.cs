using System.Collections.Generic;
using Mech.Data.LocalData;
using UnityEngine;

namespace LocalBattle3d
{
	public class ArmyObject : MonoBehaviour
	{
		public List<SquadObject> SquadObjectList;

		public void CreateSquads(ArmyLocalData armyLocalData)
		{
			for (int i = 0; i < armyLocalData.SquadLocalDataList.Count; i++)
			{
				SquadObjectList[i].CreateModels(armyLocalData.SquadLocalDataList[i]);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < SquadObjectList.Count; i++)
			{
				SquadObjectList[i].Clear();
			}
		}
	}
}