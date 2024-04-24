using System;
using UnityEngine;

public class WindowController : MonoBehaviour
{
	public GameObject WorldUi;
	public GameObject LocalBattleUI;

	public static WindowController Instance;

	public void Awake()
	{
		Instance = this;
	}

	public void SetWorldUi()
	{
		WorldUi.gameObject.SetActive(true);
		LocalBattleUI.gameObject.SetActive(false);
	}

	public void SetLocalBattleUi()
	{
		WorldUi.gameObject.SetActive(false);
		LocalBattleUI.gameObject.SetActive(true);
	}
}