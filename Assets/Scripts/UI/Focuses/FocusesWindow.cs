using System;
using System.Collections.Generic;
using Mech.Data.LocalData;
using TMPro;
using UnityEngine;

namespace Mech.UI
{
	public class FocusesWindow : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private FocusInfoWindow _focusInfoWindow;
		[SerializeField] private List<Focus> _focusList;
		[SerializeField] private TMP_Text _gloryPointsText;
		[SerializeField] private string _gloryPointsTextFormat;

		public static FocusesWindow Instance;

		private void Awake()
		{
			Instance = this;
		}

		public void Open()
		{
			SetVisible(true);
			RecheckFocuses();
			GameController.Instance.ReturnFromSquadManagementWindow();
			_gloryPointsText.text = string.Format(_gloryPointsTextFormat, PlayerData.Instance.GloryPoints);
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

		public void RecheckFocuses()
		{
			_focusList.ForEach(x => x.Check());
			_gloryPointsText.text = string.Format(_gloryPointsTextFormat, PlayerData.Instance.GloryPoints);
		}
	}
}