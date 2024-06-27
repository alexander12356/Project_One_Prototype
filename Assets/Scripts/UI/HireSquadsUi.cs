using System.Linq;
using EventBusSystem;
using Mech.Data.LocalData;
using UnityEngine;

public class HireSquadsUi : MonoBehaviour
{
	public HireSquadItemUi hireSquadItemUiPrefab;
	public Transform hireSquadItemUiHolder;
	public TextEffect TextEffectPrefab;
	public Transform TextEffectHolder;
	public CanvasGroup CanvasGroup;

	private City _city;

	public void Open(City city)
	{
		_city = city;
		CanvasGroup.alpha = 1f;
		CanvasGroup.blocksRaycasts = true;
		city.UpdateSquads();
		CreateSquads(city);
	}

	public void Close()
	{
		CanvasGroup.alpha = 0f;
		CanvasGroup.blocksRaycasts = false;
	}

	private void CreateSquads(City city)
	{
		foreach (var squad in city.SquadsLocalData)
		{
			var hireSquadItemUi = Instantiate(hireSquadItemUiPrefab, hireSquadItemUiHolder);
			hireSquadItemUi.SetData(squad, OnTryHire);
		}
	}

	private void OnTryHire(HireSquadItemUi hireSquadItemUi)
	{
		var squadCost = _city.SquadsGlobalData.FirstOrDefault(x => x.Guid == hireSquadItemUi.Data.Guid).Cost;
		if (PlayerData.Instance.Moneys >= squadCost)
		{
			PlayerData.Instance.Moneys -= squadCost;
			//PlayerData.Instance.AddNewSquad(hireSquadItemUi.Data.SquadId);
			_city.SquadsLocalData.Remove(hireSquadItemUi.Data);
			hireSquadItemUi.Dispose();
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
		}
		else
		{
			var textEffect = Instantiate(TextEffectPrefab, TextEffectHolder);
			textEffect.SetText("Not enough moneys");
		}
	}
}