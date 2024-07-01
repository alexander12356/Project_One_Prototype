using System.Collections.Generic;
using Data.Global.City;
using Mech.Data.LocalData;
using UnityEngine;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/CityGlobalData)", fileName = nameof(CityGlobalData))]
	public class CityGlobalData : ScriptableObject
	{
		[SerializeField] private string _cityName;
		[SerializeField] private StoreGlobalData _storeGlobalData;
		[SerializeField] private GuildGlobalData _guildGlobalData;

		public string GetName()
		{
			return _cityName;
		}

		public GuildGlobalData GetGuildGlobalData()
		{
			return _guildGlobalData;
		}
	}
}