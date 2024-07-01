using System;
using Mech.Data.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private string _countTextFormat;
	[SerializeField] private Image _icon;
	[SerializeField] private ItemGlobalDataList _itemGlobalData;

	private ItemType _itemType;
	private Action<StoreItem> _onClick;

	public void Init(ItemType itemType, int count, Action<StoreItem> onClick)
	{
		_itemType = itemType;
		_countText.text = string.Format(_countTextFormat, count);
		_onClick = onClick;
		_icon.sprite = _itemGlobalData.GetItemGlobalData(itemType).icon;
	}

	public ItemType GetItemType()
	{
		return _itemType;
	}

	public void Dispose()
	{
		Destroy(gameObject);
	}

	public void OnClick()
	{
		_onClick?.Invoke(this);
	}
}