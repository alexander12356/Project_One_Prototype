using System;
using System.Collections.Generic;
using Mech.Data.Global;
using UnityEngine;

namespace Mech.Data.LocalData
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
	}
}