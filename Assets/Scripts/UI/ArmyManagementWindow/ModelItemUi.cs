using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using Mech.Data.Local;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mech.UI
{
	public class ModelItemUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private Image _modelIcon;
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private GameObject _levelUpButton;
		[SerializeField] private TMP_Text _countText;
		[SerializeField] private string _countTextFormat;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;
		[SerializeField] private RectTransform _viewTransform;

		private ArmyLocalData _armyLocalData;
		private List<ModelLocalData> _modelLocalDataList;
		private int _squadId;
		private ModelType _modelType;

		public void Init(int squadId, ModelType modelType, List<ModelLocalData> modelDataList)
		{
			var modelGlobalData = _modelGlobalDataList.GetModelData(modelType);

			_modelType = modelType;
			_squadId = squadId;
			_modelLocalDataList = modelDataList;

			_modelIcon.sprite = modelGlobalData.Icon;
			_titleText.text = modelGlobalData.Title;
			_countText.text = string.Format(_countTextFormat, modelDataList.Count);
			_levelUpButton.SetActive(modelDataList.Any(x => x.IsLevelUp));
		}

		public void Upgrade()
		{
			ModelUpgradeWindow.Instance.Open(_squadId, _modelType, _modelLocalDataList.Count(x => x.IsLevelUp));
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_canvasGroup.blocksRaycasts = false;
			ArmyManagementWindow.Instance.GetSquadUiItem(_squadId).SetRaycast(false);

			_viewTransform.SetParent(ArmyManagementWindow.Instance.DragItemHolder);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_viewTransform.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_canvasGroup.blocksRaycasts = true;
			ArmyManagementWindow.Instance.GetSquadUiItem(_squadId).SetRaycast(true);

			_viewTransform.SetParent(transform, false);
			_viewTransform.offsetMin = Vector2.zero;
			_viewTransform.offsetMax = Vector2.zero;

			var squadItemUi = eventData.hovered.LastOrDefault(x => x.GetComponent<SquadItemUi>() != null)?.GetComponent<SquadItemUi>();
			var newSquadId = squadItemUi == null ? _squadId : squadItemUi.SquadId;

			if (_squadId == newSquadId)
			{
				return;
			}

			ArmyManagementWindow.Instance.MoveModelsTo(_squadId, _modelType, newSquadId);
		}
	}
}