using DefaultNamespace;
using UnityEngine;

public class SquadObject : MonoBehaviour
{
	public SpriteRenderer Icon;

	public void SetData(PlayerData.SquadLocalData squadData)
	{
		Icon.sprite = BalanceController.Instance.GetSquadGlobalData(squadData.Id).Icon;
	}
}
