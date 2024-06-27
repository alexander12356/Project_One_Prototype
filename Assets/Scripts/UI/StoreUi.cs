using EventBusSystem;
using Mech.Data.LocalData;
using TMPro;
using UnityEngine;

public class StoreUi : MonoBehaviour
{
	public Item StoreSuppliesItem;
	public Item PlayerSuppliesItem;
	public TextEffect TextEffectPrefab;
	public Transform TextEffectHolder;
	public TMP_Text StoreMoneyText;
	public CanvasGroup CanvasGroup;

	private City _city;

	public void Open(City city)
	{
		_city = city;
		city.UpdateStore();
		StoreSuppliesItem.SetCount(city.SuppliesCount);
		PlayerSuppliesItem.SetCount(PlayerData.Instance.Supplies);
		StoreMoneyText.text = $"${city.StoreMoneys}";
		CanvasGroup.alpha = 1f;
		CanvasGroup.blocksRaycasts = true;

		UpdateItemView();
	}

	public void Close()
	{
		CanvasGroup.alpha = 0f;
		CanvasGroup.blocksRaycasts = false;
	}

	public void OnSuppliesBuy()
	{
		if (PlayerData.Instance.Moneys >= _city.SuppliesBuyCost)
		{
			StoreSuppliesItem.SetCount(_city.SuppliesCount - 1);
			PlayerSuppliesItem.SetCount(PlayerData.Instance.Supplies + 1);
			_city.StoreMoneys += _city.SuppliesBuyCost;
			_city.SuppliesCount--;
			PlayerData.Instance.Moneys -= _city.SuppliesBuyCost;
			PlayerData.Instance.Supplies++;
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowSupplies(PlayerData.Instance.Supplies));
			StoreMoneyText.text = $"${_city.StoreMoneys}";
		}
		else
		{
			var textEffect = Instantiate(TextEffectPrefab, TextEffectHolder);
			textEffect.SetText("Need more money");
		}

		UpdateItemView();
	}

	public void OnSuppliesSell()
	{
		if (_city.StoreMoneys >= _city.SuppliesSellCost)
		{
			StoreSuppliesItem.SetCount(_city.SuppliesCount + 1);
			PlayerSuppliesItem.SetCount(PlayerData.Instance.Supplies - 1);
			_city.StoreMoneys -= _city.SuppliesSellCost;
			_city.SuppliesCount++;
			PlayerData.Instance.Moneys += _city.SuppliesSellCost;
			PlayerData.Instance.Supplies--;
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowSupplies(PlayerData.Instance.Supplies));
			StoreMoneyText.text = $"${_city.StoreMoneys}";
		}
		else
		{
			var textEffect = Instantiate(TextEffectPrefab, TextEffectHolder);
			textEffect.SetText("Store not have a money");
		}
		
		UpdateItemView();
	}

	private void UpdateItemView()
	{
		StoreSuppliesItem.gameObject.SetActive(_city.SuppliesCount > 0);
		PlayerSuppliesItem.gameObject.SetActive(PlayerData.Instance.Supplies > 0);
	}
}