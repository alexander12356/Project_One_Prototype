using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class City : MonoBehaviour
{
	[Serializable]
	public class StoreGlobalData
	{
		public int StoreMoneys;
		public int SuppliesCount;
		public int SuppliesBuyCost;
		public int SuppliesSellCost;
	}

	[Serializable]
	public class StoreLocalData
	{
		public int StoreMoneys;
		public int SuppliesCount;
	}

	[Serializable]
	public class SquadGlobalData
	{
		public string Guid;
		public string SquadId;
		public int Cost;
	}

	[Serializable]
	public class SquadLocalData
	{
		public string Guid;
		public string SquadId;
	}

	public string CityName;
	public TMP_Text CityCaption;
	public Transform EnterPositionTransform;
	public Transform ExitPositionTransform;
	public StoreGlobalData CityStoreGlobalData;
	public StoreLocalData CityStoreLocalData;

	public Vector3 ExitPosition => ExitPositionTransform.position;
	public Vector3 EnterPosition => EnterPositionTransform.position;

	public int SuppliesCount
	{
		get => CityStoreLocalData.SuppliesCount;
		set => CityStoreLocalData.SuppliesCount = value;
	}

	public int StoreMoneys
	{
		get => CityStoreLocalData.StoreMoneys;
		set => CityStoreLocalData.StoreMoneys = value;
	}

	public int SuppliesBuyCost => CityStoreGlobalData.SuppliesBuyCost;
	public int SuppliesSellCost => CityStoreGlobalData.SuppliesSellCost;
	public List<SquadGlobalData> SquadsGlobalData;
	public List<SquadLocalData> SquadsLocalData;

	[FormerlySerializedAs("DurationBeforeUpdate")] public string DurationBeforeUpdateStore;
	public string DurationBeforeUpdateSquads;

	private DateTime PreviousStoreVisitTime;
	private DateTime PreviousSquadsVisitTime;

	public void Start()
	{
		CityCaption.text = CityName;
	}

	public void UpdateStore()
	{
		var durationBeforeUpdateTimeSpan = TimeSpan.Parse(DurationBeforeUpdateStore);
		var updateDateTime = PreviousStoreVisitTime.Add(durationBeforeUpdateTimeSpan);
		var currentTime = GameController.Instance.CurrentDateTime;
		if (currentTime >= updateDateTime)
		{
			CityStoreLocalData.StoreMoneys = CityStoreGlobalData.StoreMoneys;
			CityStoreLocalData.SuppliesCount = CityStoreGlobalData.SuppliesCount;
		}

		PreviousStoreVisitTime = currentTime;
	}

	public void UpdateSquads()
	{
		var durationBeforeUpdateTimeSpan = TimeSpan.ParseExact(DurationBeforeUpdateSquads, @"dd\:hh", CultureInfo.InvariantCulture);
		var updateDateTime = PreviousSquadsVisitTime.Add(durationBeforeUpdateTimeSpan);
		var currentTime = GameController.Instance.CurrentDateTime;
		if (currentTime >= updateDateTime)
		{
			SquadsLocalData.Clear();
			foreach (var squadData in SquadsGlobalData)
			{
				SquadsLocalData.Add(new SquadLocalData
				{
					Guid = squadData.Guid,
					SquadId = squadData.SquadId
				});
			}
		}

		PreviousSquadsVisitTime = currentTime;
	}

	[ContextMenu("Update guids for squads global data")]
	public void UpdateGuidsForGlobalData()
	{
		foreach (var squadData in SquadsGlobalData)
		{
			squadData.Guid = Guid.NewGuid().ToString();
		}
	}
}