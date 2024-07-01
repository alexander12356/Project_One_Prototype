using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/ItemGlobalDataList", fileName = nameof(ItemGlobalDataList))]
	public class ItemGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<ItemType, ItemGlobalData> _items;

		public ItemGlobalData GetItemGlobalData(ItemType itemType)
		{
			return _items[itemType];
		}
	}
}