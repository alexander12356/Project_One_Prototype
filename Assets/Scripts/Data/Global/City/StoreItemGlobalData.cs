using System;

namespace Mech.Data.Global
{
	[Serializable]
	public struct StoreItemGlobalData
	{
		public ItemType ItemType;
		public int Count;
		public int BuyCost;
		public int SellCost;
	}
}