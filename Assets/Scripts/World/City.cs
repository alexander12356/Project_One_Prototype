using System;
using System.Globalization;
using Data.Global.City;
using EventBusSystem;
using Mech.Data.Global;
using Mech.Data.LocalData;
using Mech.World;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class City : MonoBehaviour
{
	[SerializeField] private TMP_Text _cityNameText;
	[SerializeField] private string _cityNameTextFormat;
	[SerializeField] private Transform _enterPositionTransform;
	[SerializeField] private Transform _exitPositionTransform;
	[SerializeField] private CityGlobalData _cityGlobalData;
	[SerializeField, ReadOnly] private CityLocalData _cityLocalData;

	private StoreLocalData CityStoreLocalData;

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

	[FormerlySerializedAs("DurationBeforeUpdate")]
	public string DurationBeforeUpdateStore;

	public string DurationBeforeUpdateSquads;

	private DateTime PreviousCityVisitTime;

	public void Start()
	{
		_cityNameText.text = string.Format(_cityNameTextFormat, _cityGlobalData.GetName());
	}

	public void Visit()
	{
		Player.Instance.SetVisible(false);
		Player.Instance.SetPosition(_enterPositionTransform.position);
		EventBus.RaiseEvent<IWorldUi>(x => x.OpenCityUi(gameObject));

		UpdateCity();
	}

	public void Leave()
	{
		Player.Instance.SetVisible(true);
		Player.Instance.SetPosition(_exitPositionTransform.position);
		EventBus.RaiseEvent<IGameController>(x => x.CloseCity());
	}

	private void UpdateCity()
	{
		UpdateGuild();
		UpdateStore();
		PreviousCityVisitTime = GameController.Instance.CurrentDateTime;
		return;

		void UpdateGuild()
		{
			var durationBeforeUpdateTimeSpan = TimeSpan.ParseExact(DurationBeforeUpdateSquads, @"dd\:hh", CultureInfo.InvariantCulture);
			var updateDateTime = PreviousCityVisitTime.Add(durationBeforeUpdateTimeSpan);
			var currentTime = GameController.Instance.CurrentDateTime;
			if (currentTime >= updateDateTime)
			{
				_cityLocalData.GuildLocalData.Clear();
				foreach (var guildItemGlobalData in _cityGlobalData.GetGuildGlobalData().GetGuildItemGlobalDataList())
				{
					_cityLocalData.GuildLocalData.GetGuildItemLocalDataList().Add(guildItemGlobalData.ModelType, guildItemGlobalData.Count);
				}
			}
		}

		void UpdateStore()
		{
			/*
			var durationBeforeUpdateTimeSpan = TimeSpan.Parse(DurationBeforeUpdateStore);
			var updateDateTime = PreviousStoreVisitTime.Add(durationBeforeUpdateTimeSpan);
			var currentTime = GameController.Instance.CurrentDateTime;
			if (currentTime >= updateDateTime)
			{
				//CityStoreLocalData.StoreMoneys = CityStoreGlobalData.StoreMoneys;
				//CityStoreLocalData.SuppliesCount = CityStoreGlobalData.SuppliesCount;
			}

			PreviousStoreVisitTime = currentTime;
			*/
		}
	}

	public CityLocalData GetCityLocalData()
	{
		return _cityLocalData;
	}

	public CityGlobalData GetCityGlobalData()
	{
		return _cityGlobalData;
	}
}