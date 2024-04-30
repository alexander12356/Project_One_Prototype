using System;
using TMPro;
using UnityEngine;

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

	public string DurationBeforeUpdate;

	private DateTime PreviousStoreVisitTime;

	public void Start()
	{
		CityCaption.text = CityName;
	}

	public void UpdateStore()
	{
		var durationBeforeUpdateTimeSpan = TimeSpan.Parse(DurationBeforeUpdate);
		var updateDateTime = PreviousStoreVisitTime.Add(durationBeforeUpdateTimeSpan);
		var currentTime = GameController.Instance.CurrentDateTime;
		if (currentTime >= updateDateTime)
		{
			CityStoreLocalData.StoreMoneys = CityStoreGlobalData.StoreMoneys;
			CityStoreLocalData.SuppliesCount = CityStoreGlobalData.SuppliesCount;
		}

		PreviousStoreVisitTime = currentTime;
	}
}