using System.Collections.Generic;
using EventBusSystem;
using Mech.Data.Global;
using Mech.Data.Local;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class StoreUi : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private TMP_Text _storeMoneysText;
	[SerializeField] private string _storeMoneysTextFormat;
	[SerializeField] private StoreItem _storeStoreItemPrefab;
	[SerializeField] private RectTransform _storeItemsHolder;
	[SerializeField] private RectTransform _playerItemsHolder;
	[SerializeField] private TextEffect _textEffectPrefab;
	[SerializeField] private Transform _textEffectHolder;

	private StoreLocalData _storeLocalData;
	private StoreGlobalData _storeGlobalData;
	private Dictionary<ItemType, StoreItem> _storeItemList = new ();
	private Dictionary<ItemType, StoreItem> _playerItemList = new ();

	public void Open(City city)
	{
		SetVisible(true);

		_storeLocalData = city.GetCityLocalData().StoreLocalData;
		_storeGlobalData = city.GetCityGlobalData().GetStoreGlobalData();

		UpdateStoreMoneys();
		CreateStoreItems(_storeLocalData);
		CreatePlayerItems();

		return;

		void CreateStoreItems(StoreLocalData storeLocalData)
		{
			foreach (var itemType in storeLocalData.Items.Keys)
			{
				if (storeLocalData.Items[itemType] <= 0)
				{
					continue;
				}

				var storeItem = Instantiate(_storeStoreItemPrefab, _storeItemsHolder);
				storeItem.Init(itemType, storeLocalData.Items[itemType], OnTryBuy);
				_storeItemList.Add(itemType, storeItem);
			}
		}

		void CreatePlayerItems()
		{
			foreach (var itemLocalData in PlayerData.Instance.InventoryLocalData.Items)
			{
				if (itemLocalData.Count <= 0)
				{
					continue;
				}

				var storeItem = Instantiate(_storeStoreItemPrefab, _playerItemsHolder);
				storeItem.Init(itemLocalData.ItemType, itemLocalData.Count, OnTrySell);
				_playerItemList.Add(itemLocalData.ItemType, storeItem);
			}
		}
	}

	public void Close()
	{
		_storeItemList.ForEach(x => Destroy(x.Value.gameObject));
		_playerItemList.ForEach(x => Destroy(x.Value.gameObject));
		_storeItemList.Clear();
		_playerItemList.Clear();
		SetVisible(false);
	}

	private void SetVisible(bool value)
	{
		_canvasGroup.alpha = value ? 1f : 0f;
		_canvasGroup.blocksRaycasts = value;
	}

	private void UpdateStoreMoneys()
	{
		_storeMoneysText.text = string.Format(_storeMoneysTextFormat, _storeLocalData.StoreMoneys);
	}

	private void OnTryBuy(StoreItem storeItem)
	{
		var itemType = storeItem.GetItemType();
		var itemBuyCost = _storeGlobalData.GetItemBuyCost(itemType);
		if (PlayerData.Instance.Moneys >= itemBuyCost)
		{
			PlayerData.Instance.Moneys -= itemBuyCost;
			PlayerData.Instance.InventoryLocalData.AddItem(itemType, 1);
			_storeLocalData.RemoveItem(itemType, 1);
			if (_storeLocalData.Items[itemType] <= 0)
			{
				_storeItemList.Remove(itemType);
				storeItem.Dispose();
			}
			else
			{
				storeItem.Init(itemType, _storeLocalData.Items[itemType], OnTryBuy);
			}
			_playerItemList[itemType].Init(itemType, PlayerData.Instance.InventoryLocalData.GetItemCount(itemType), OnTrySell);

			UpdateWorldUi();
			UpdateStoreMoneys();
		}
		else
		{
			var textEffect = Instantiate(_textEffectPrefab, _textEffectHolder);
			textEffect.SetText("Not enough moneys");
		}
	}

	private void OnTrySell(StoreItem storeItem)
	{
		var itemType = storeItem.GetItemType();
		var itemSellCost = _storeGlobalData.GetItemSellCost(itemType);
		if (_storeLocalData.StoreMoneys >= itemSellCost)
		{
			_storeLocalData.StoreMoneys -= itemSellCost;
			_storeLocalData.AddItem(itemType, 1);
			PlayerData.Instance.InventoryLocalData.RemoveItem(itemType, 1);
			if (PlayerData.Instance.InventoryLocalData.GetItemCount(itemType) <= 0)
			{
				storeItem.Dispose();
			}
			else
			{
				_playerItemList[itemType].Init(itemType, PlayerData.Instance.InventoryLocalData.GetItemCount(itemType), OnTrySell);
			}
			_storeItemList[itemType].Init(itemType, _storeLocalData.Items[itemType], OnTryBuy);

			UpdateWorldUi();
			UpdateStoreMoneys();
		}
		else
		{
			var textEffect = Instantiate(_textEffectPrefab, _textEffectHolder);
			textEffect.SetText("Not enough moneys");
		}
	}

	private void UpdateWorldUi()
	{
		EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
		EventBus.RaiseEvent<IWorldUi>(x => x.ShowSupplies(PlayerData.Instance.InventoryLocalData.GetItemCount(ItemType.Supply)));
	}
}