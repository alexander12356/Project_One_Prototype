using System;
using System.Globalization;
using EventBusSystem;
using Mech.Data.Global;
using Mech.Data.Local;
using Mech.World;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class City : MonoBehaviour
{
	[SerializeField] private TMP_Text _cityNameText;
	[SerializeField] private string _cityNameTextFormat;
	[SerializeField] private Transform _enterPositionTransform;
	[SerializeField] private Transform _exitPositionTransform;
	[SerializeField] private CityGlobalData _cityGlobalData;
	[SerializeField, ReadOnly] private CityLocalData _cityLocalData;

	private DateTime PreviousGuildUpdateTime;
	private DateTime PreviousStoreUpdateTime;

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
		return;

		void UpdateGuild()
		{
			var durationBeforeUpdateTimeSpan = TimeSpan.ParseExact(_cityGlobalData.GetGuildGlobalData().GetGuildUpdateDuration(), @"dd\:hh", CultureInfo.InvariantCulture);
			var updateDateTime = PreviousGuildUpdateTime.Add(durationBeforeUpdateTimeSpan);
			var currentTime = GameController.Instance.CurrentDateTime;
			if (currentTime >= updateDateTime)
			{
				PreviousGuildUpdateTime = updateDateTime;
				_cityLocalData.GuildLocalData.Clear();
				foreach (var guildItemGlobalData in _cityGlobalData.GetGuildGlobalData().GetGuildItemGlobalDataList())
				{
					_cityLocalData.GuildLocalData.GetGuildItemLocalDataList().Add(guildItemGlobalData.ModelType, guildItemGlobalData.Count);
				}
			}
		}

		void UpdateStore()
		{
			var durationBeforeUpdateTimeSpan = TimeSpan.Parse(_cityGlobalData.GetStoreGlobalData().GetStoreUpdateDuration());
			var updateDateTime = PreviousStoreUpdateTime.Add(durationBeforeUpdateTimeSpan);
			var currentTime = GameController.Instance.CurrentDateTime;
			if (currentTime >= updateDateTime)
			{
				PreviousStoreUpdateTime = updateDateTime;
				_cityLocalData.StoreLocalData.Clear();
				foreach (var storeItemGlobalData in _cityGlobalData.GetStoreGlobalData().GetStoreItemGlobalDataList())
				{
					_cityLocalData.StoreLocalData.Items.Add(storeItemGlobalData.ItemType, storeItemGlobalData.Count);
				}
			}
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