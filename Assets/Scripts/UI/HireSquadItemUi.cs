using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class HireSquadItemUi : MonoBehaviour
{
	public Image _iconImage;
	public City.SquadLocalData Data;
	public Action<HireSquadItemUi> TryHireAction;

	public void SetData(City.SquadLocalData data, Action<HireSquadItemUi> onTryHire)
	{
		Data = data;
		TryHireAction = onTryHire;
		_iconImage.sprite = BalanceController.Instance.Balances.Balances.FirstOrDefault(x => x.Id == data.SquadId).Icon;
	}

	public void HireButtonPress()
	{
		TryHireAction?.Invoke(this);
	}

	public void Dispose()
	{
		Destroy(gameObject);
	}
}