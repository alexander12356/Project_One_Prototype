using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour
{
	[Serializable]
	public class SquadLocalData
	{
		public string Guid;
		public string Id;
		[FormerlySerializedAs("Toughness")] public int Wound;
		public int Exp;
		public bool IsLevelUp;

		public void SetGuid(string newGuid)
		{
			Guid = newGuid;
		}
	}
	
	public List<SquadLocalData> Squad;

	public static PlayerData Instance;
	public int Supplies;

	public void Awake()
	{
		Instance = this;
	}

	[ContextMenu("Init")]
	public void SetGuids()
	{
		for (int i = 0; i < Squad.Count; i++)
		{
			Squad[i] = new SquadLocalData()
			{
				Guid = Guid.NewGuid().ToString(),
				Id = Squad[i].Id
			};
		}
	}
}