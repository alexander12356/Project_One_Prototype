using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SquadImprovmentUi : MonoBehaviour
{
	[Serializable]
	public struct ImprovmentList
	{
		public string Id;
		public List<string> PrevIds;
		[FormerlySerializedAs("NextId")] public List<string> NextIds;
	}

	[Serializable]
	public struct ImprovmentView
	{
		public string Id;
		public Toggle Toggle;
		public Image Icon;
	}

	public static SquadImprovmentUi Instance;

	public CanvasGroup CanvasGroup;
	public List<ImprovmentList> ImprovmentLists;
	public List<ImprovmentView> ImprovmentViews;

	private PlayerData.SquadLocalData _squadLocalData;
	private bool _isLevelUp;
	private ImprovmentList _improvmentList;

	private void Awake()
	{
		Instance = this;
	}

	public void Open(PlayerData.SquadLocalData squadLocalData)
	{
		CanvasGroup.alpha = 1f;
		CanvasGroup.blocksRaycasts = true;

		_squadLocalData = squadLocalData;

		foreach (var view in ImprovmentViews)
		{
			view.Icon.sprite = BalanceController.Instance.GetSquadGlobalData(view.Id).Icon;
			view.Toggle.interactable = false;
			view.Toggle.isOn = false;
		}

		_improvmentList = ImprovmentLists.FirstOrDefault(x => x.Id == squadLocalData.Id);
		foreach (var id in _improvmentList.PrevIds)
		{
			ImprovmentViews.FirstOrDefault(x => x.Id == id).Toggle.isOn = true;
		}

		if (squadLocalData.IsLevelUp)
		{
			foreach (var nextId in _improvmentList.NextIds)
			{
				ImprovmentViews.FirstOrDefault(x => x.Id == nextId).Toggle.interactable = true;
			}
		}
	}

	public void Accept()
	{
		if (!_isLevelUp)
		{
			return;
		}

		var newId = string.Empty;
		foreach (var nextId in _improvmentList.NextIds)
		{
			if (ImprovmentViews.FirstOrDefault(x => x.Id == nextId).Toggle.isOn)
			{
				newId = nextId;
			}
		}

		_squadLocalData.Id = newId;
		_squadLocalData.IsLevelUp = false;
		_squadLocalData.Exp = 0;

		Close();
	}

	public void Close()
	{
		CanvasGroup.alpha = 0f;
		CanvasGroup.blocksRaycasts = false;
	}

	public void LevelUp(bool value)
	{
		foreach (var nextId in _improvmentList.NextIds)
		{
			ImprovmentViews.FirstOrDefault(x => x.Id == nextId).Toggle.isOn = false;
		}

		_isLevelUp = value;
	}
}