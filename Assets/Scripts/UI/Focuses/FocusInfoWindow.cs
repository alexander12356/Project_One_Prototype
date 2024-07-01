using System;
using Mech.Data.Local;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class FocusInfoWindow : MonoBehaviour
	{
		public static FocusInfoWindow Instance;
		
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private Button _exploreButton;
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private TMP_Text _loreDescription;
		[SerializeField] private TMP_Text _statDescription;
		[SerializeField] private GameObject _reachedText;

		private Focus _focus;
		private int _cost;

		private void Awake()
		{
			Instance = this;
		}

		public void Open(GameObject focusObject)
		{
			SetVisible(true);
			_focus = focusObject.GetComponent<Focus>();
			_exploreButton.interactable = _focus.CanExplore && !_focus.IsExplored;
			_titleText.text = _focus.Title;
			_loreDescription.text = _focus.Lore;
			_statDescription.text = _focus.Stat;
			_cost = _focus.Cost;
			_reachedText.gameObject.SetActive(_focus.IsExplored);
		}

		public void Explore()
		{
			if (_cost <= PlayerData.Instance.GloryPoints)
			{
				PlayerData.Instance.GloryPoints -= _cost;
				_exploreButton.interactable = false;
				_reachedText.gameObject.SetActive(true);
				_focus.IsExplored = true;
				SetVisible(false);
				FocusesWindow.Instance.RecheckFocuses();
			}
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
	}
}