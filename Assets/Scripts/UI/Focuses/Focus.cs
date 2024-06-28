using System;
using System.Collections.Generic;
using Mech.Data.Global;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FocusType = Mech.Data.Global.FocusType;

namespace Mech.UI
{
	public class Focus : MonoBehaviour
	{
		[SerializeField] private FocusType _focusType;
		[SerializeField] private FocusGlobalDataList _focusGlobalDataList;
		[SerializeField] private List<Focus> _xorFocusList;
		[SerializeField] private List<Focus> _orFocusList;
		[SerializeField] private List<Focus> _allFocusList;
		[SerializeField] private Image _frameImage;
		[SerializeField] private Color _canExploreColor;
		[SerializeField] private Color _exploredColor;
		[SerializeField] private Color _blockedColor;

		public string Title => _focusGlobalDataList.GetFocusGlobalData(_focusType).Title;
		public string Lore => _focusGlobalDataList.GetFocusGlobalData(_focusType).LoreDescription;
		public string Stat => _focusGlobalDataList.GetFocusGlobalData(_focusType).StatDescription;
		public int Cost => _focusGlobalDataList.GetFocusGlobalData(_focusType).Cost;
		public bool IsExplored;
		public bool CanExplore;

		public void Check()
		{
			if (IsExplored)
			{
				_frameImage.color = _exploredColor;
				return;
			}

			CanExplore = true;
			_frameImage.color = _canExploreColor;

			foreach (var focus in _xorFocusList)
			{
				if (focus.IsExplored)
				{
					CanExplore = false;
					_frameImage.color = _blockedColor;
					return;
				}
			}

			if (_orFocusList.Count > 0)
			{
				CanExplore = false;
				_frameImage.color = _blockedColor;
				foreach (var focus in _orFocusList)
				{
					if (focus.IsExplored)
					{
						CanExplore = true;
						_frameImage.color = _canExploreColor;
						return;
					}
				}
			}

			if (_allFocusList.Count > 0)
			{
				CanExplore = true;
				foreach (var focus in _allFocusList)
				{
					CanExplore &= focus.IsExplored;
				}

				_frameImage.color = CanExplore ? _canExploreColor : _blockedColor;
			}
		}

		public void OpenFocus()
		{
			FocusInfoWindow.Instance.Open(gameObject);
		}
	}
}