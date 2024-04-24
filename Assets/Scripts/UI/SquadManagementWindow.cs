using DefaultNamespace;
using UnityEngine;

public class SquadManagementWindow : MonoBehaviour
{
	public SquadStatsUi SquadStatsUiPrefab;
	public Transform SquadsHolder;
	public CanvasGroup CanvasGroup;

	public static SquadManagementWindow Instance;

	public void Awake()
	{
		Instance = this;
	}

	public void Open()
	{
		CanvasGroup.alpha = 1f;
		CanvasGroup.blocksRaycasts = true;
		for (int i = 0; i < SquadsHolder.childCount; i++)
		{
			Destroy(SquadsHolder.GetChild(i).gameObject);
		}
		foreach (var squad in PlayerData.Instance.Squad)
		{
			var statsUi = Instantiate(SquadStatsUiPrefab, SquadsHolder);
			statsUi.SetStats(BalanceController.Instance.GetSquadGlobalData(squad.Id));
			statsUi.SetToughness(squad.Toughness);
		}
	}

	public void Close()
	{
		CanvasGroup.alpha = 0f;
		CanvasGroup.blocksRaycasts = false;
		GameController.Instance.ReturnFromSquadManagementWindow();
	}
}