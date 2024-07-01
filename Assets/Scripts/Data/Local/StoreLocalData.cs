using System;
using Mech.Data.Global;
using UnityEngine.Rendering;

namespace Mech.Data.Local
{
	[Serializable]
	public class StoreLocalData
	{
		public int StoreMoneys;
		public SerializedDictionary<ItemType, int> Items;

		public void RemoveItem(ItemType itemType, int count)
		{
			Items[itemType] -= count;
		}

		public void AddItem(ItemType itemType, int count)
		{
			if (Items.ContainsKey(itemType))
			{
				Items[itemType] += count;
			}
			Items.Add(itemType, count);
		}

		public void Clear()
		{
			Items.Clear();
		}
	}
}