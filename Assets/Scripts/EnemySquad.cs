using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

public class EnemySquad : MonoBehaviour
{
	public List<PlayerData.SquadLocalData> Squad;

	public List<PlayerData.SquadLocalData> GetLocalSquadData() => Squad;

	[ContextMenu("Init")]
	public void SetGuids()
	{
		for (int i = 0; i < Squad.Count; i++)
		{
			Squad[i] = new PlayerData.SquadLocalData()
			{
				Guid = Guid.NewGuid().ToString(),
				Id = Squad[i].Id
			};
		}
	}
}
