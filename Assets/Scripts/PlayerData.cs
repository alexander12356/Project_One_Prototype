using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour
{
	[Serializable]
	public struct SquadLocalData
	{
		public string Guid;
		public string Id;
		[FormerlySerializedAs("Toughness")] public int Wound;

		public void SetGuid(string newGuid)
		{
			Guid = newGuid;
		}
	}
	
	public List<SquadLocalData> Squad;

	public static PlayerData Instance;

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