using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;

namespace Mech.Data.Local
{
	[Serializable]
	public class InventoryLocalData
	{
		public List<ItemLocalData> Items;

		public void AddItem(ItemType itemType, int count)
		{
			var itemLocalData = Items.FirstOrDefault(x => x.ItemType == itemType);
			if (itemLocalData == null)
			{
				Items.Add(new ItemLocalData
				{
					ItemType = itemType,
					Count = count
				});
				return;
			}

			itemLocalData.Count += count;
		}

		public void RemoveItem(ItemType itemType, int count)
		{
			var itemLocalData = Items.FirstOrDefault(x => x.ItemType == itemType);
			if (itemLocalData == null)
			{
				return;
			}
				
			itemLocalData.Count -= count;
		}

		public int GetItemCount(ItemType itemType)
		{
			var itemLocalData = Items.FirstOrDefault(x => x.ItemType == itemType);
			return itemLocalData?.Count ?? 0;
		}

		public void SetItemCount(ItemType itemType, int count)
		{
			var itemLocalData = Items.FirstOrDefault(x => x.ItemType == itemType);
			if (itemLocalData == null)
			{
				Items.Add(new ItemLocalData
				{
					ItemType = itemType,
					Count = count
				});
				return;
			}

			itemLocalData.Count = count;
		}

		public ItemLocalData GetItemLocalData(ItemType itemType)
		{
			return Items.FirstOrDefault(x => x.ItemType == itemType);
		}
	}
}