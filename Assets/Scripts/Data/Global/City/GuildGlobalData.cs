﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using UnityEngine;

namespace Data.Global.City
{
	[Serializable]
	public class GuildGlobalData
	{
		[SerializeField] private List<GuildItemGlobalData> _guildItemGlobalDataList;
		[SerializeField] private string _guildUpdateDuration;

		public List<GuildItemGlobalData> GetGuildItemGlobalDataList()
		{
			return _guildItemGlobalDataList;
		}

		public int GetItemCost(ModelType modelType)
		{
			return _guildItemGlobalDataList.FirstOrDefault(x => x.ModelType == modelType).Cost;
		}

		public string GetGuildUpdateDuration()
		{
			return _guildUpdateDuration;
		}
	}
}