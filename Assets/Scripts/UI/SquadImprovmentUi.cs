using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Mech.Data.LocalData;
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

	private ArmyLocalData _armyLocalData;
	private ImprovmentList _improvmentList;
	private bool _isInit = false;
	private string _newId;

	private void Awake()
	{
		Instance = this;
	}

	public void Open(ArmyLocalData armyLocalData)
	{
		/*
		_isInit = false;
		
		CanvasGroup.alpha = 1f;
		CanvasGroup.blocksRaycasts = true;

		_armyLocalData = armyLocalData;

		foreach (var view in ImprovmentViews)
		{
			view.Icon.sprite = BalanceController.Instance.GetSquadGlobalData(view.Id).Icon;
			view.Toggle.interactable = false;
			view.Toggle.isOn = false;
		}

		_improvmentList = ImprovmentLists.FirstOrDefault(x => x.Id == armyLocalData.Id);
		foreach (var id in _improvmentList.PrevIds)
		{
			ImprovmentViews.FirstOrDefault(x => x.Id == id).Toggle.isOn = true;
		}

		if (armyLocalData.IsLevelUp)
		{
			foreach (var nextId in _improvmentList.NextIds)
			{
				ImprovmentViews.FirstOrDefault(x => x.Id == nextId).Toggle.interactable = true;
			}
		}

		_isInit = true;
		*/
	}

	public void Accept()
	{
		if (string.IsNullOrEmpty(_newId))
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

		/*
		_armyLocalData.Id = newId;
		_armyLocalData.IsLevelUp = false;
		_armyLocalData.Exp = 0;
		*/

		Close();
	}

	public void Close()
	{
		CanvasGroup.alpha = 0f;
		CanvasGroup.blocksRaycasts = false;
	}

	public void LevelUp(string id)
	{
		var value = ImprovmentViews.FirstOrDefault(x => x.Id == id).Toggle.isOn;
		if (value)
		{
			_newId = id;
			return;
		}
		_newId = string.Empty;
	}
}