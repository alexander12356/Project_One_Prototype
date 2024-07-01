using Data.Global.City;
using EventBusSystem;
using Mech.Data.Global;
using Mech.Data.LocalData;
using UnityEngine;

public class GuildUi : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] GuildItemUi _guildItemUiPrefab;
	[SerializeField] private Transform _itemsHolder;
	[SerializeField] private TextEffect _textEffectPrefab;
	[SerializeField] private Transform _textEffectHolder;

	private GuildGlobalData _guildGlobalData;
	private GuildLocalData _guildLocalData;

	public void Open(City city)
	{
		_guildGlobalData = city.GetCityGlobalData().GetGuildGlobalData();
		_guildLocalData = city.GetCityLocalData().GuildLocalData;

		SetVisible(true);
		CreateModelList();
	}

	public void Close()
	{
		SetVisible(false);
	}

	private void SetVisible(bool value)
	{
		_canvasGroup.alpha = value ? 1f : 0f;
		_canvasGroup.blocksRaycasts = value;
	}

	private void CreateModelList()
	{
		foreach (var guildItemLocalData in _guildLocalData.GetGuildItemLocalDataList())
		{
			for (var i = 0; i < guildItemLocalData.Value; i++)
			{
				var hireSquadItemUi = Instantiate(_guildItemUiPrefab, _itemsHolder);
				hireSquadItemUi.SetData(guildItemLocalData.Key, OnTryHire);
			}
		}
	}

	private void OnTryHire(GuildItemUi guildItemUi)
	{
		var modelType = guildItemUi.GetModelType();
		var guildItemCost = _guildGlobalData.GetItemCost(modelType);
		if (PlayerData.Instance.Moneys >= guildItemCost)
		{
			PlayerData.Instance.Moneys -= guildItemCost;
			PlayerData.Instance.ArmyLocalData.AddModel(modelType);
			_guildLocalData.RemoveItem(modelType);
			guildItemUi.Dispose();
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
		}
		else
		{
			var textEffect = Instantiate(_textEffectPrefab, _textEffectHolder);
			textEffect.SetText("Not enough moneys");
		}
	}
}