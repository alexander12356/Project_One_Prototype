using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using UnityEngine;

namespace Mech.Data.Local
{
	[Serializable]
	public class StoreGlobalData
	{
		[SerializeField] private int _storeMoneys;
		[SerializeField] private string _storeUpdateDuration;
		[SerializeField] private List<StoreItemGlobalData> _storeItemGlobalDataList;

		public string GetStoreUpdateDuration()
		{
			return _storeUpdateDuration;
		}

		public int GetStoreMoney()
		{
			return _storeMoneys;
		}

		public List<StoreItemGlobalData> GetStoreItemGlobalDataList()
		{
			return _storeItemGlobalDataList;
		}

		public int GetItemBuyCost(ItemType itemType)
		{
			return _storeItemGlobalDataList.FirstOrDefault(x => x.ItemType == itemType).BuyCost;
		}

		public int GetItemSellCost(ItemType itemType)
		{
			return _storeItemGlobalDataList.FirstOrDefault(x => x.ItemType == itemType).SellCost;
		}
	}
}