using Mech.Data.Global;
using Mech.Data.Local;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class MoveModelToNewSquadWindow : MonoBehaviour
	{
		public static MoveModelToNewSquadWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private ModelUpgradeItem _modelUpgradeItem;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;
		[SerializeField] private TMP_Text _squadTitleText1;
		[SerializeField] private TMP_Text _squadTitleText2;
		[SerializeField] private string _squadTitleTextFormat;
		[SerializeField] private TMP_Text _squadCapacityText1;
		[SerializeField] private TMP_Text _squadCapacityText2;
		[SerializeField] private string _squadCapacityTextFormat;
		[SerializeField] private TMP_Text _movedModelCountText;

		private int _canModelCountToMove;
		private int _resultModelCountToMove;
		private int _fromSquadId;
		private int _toSquad;
		private ModelType _modelType;

		private void Awake()
		{
			Instance = this;
		}

		public void Open(int squadId, ModelType modelType, int newSquadId)
		{
			SetVisible(true);

			_fromSquadId = squadId;
			_toSquad = newSquadId;
			_modelType = modelType;

			_modelUpgradeItem.Init(_modelGlobalDataList.GetModelData(modelType));
			_modelUpgradeItem.SetActive(false);

			_squadTitleText1.text = string.Format(_squadTitleTextFormat, squadId + 1);
			_squadTitleText2.text = string.Format(_squadTitleTextFormat, newSquadId + 1);

			_squadCapacityText1.text = string.Format(_squadCapacityTextFormat, PlayerData.Instance.ArmyLocalData.SquadLocalDataList[squadId].ModelLocalDataList.Count, PlayerData.Instance.SquadMaxCapacity);
			_squadCapacityText2.text = string.Format(_squadCapacityTextFormat, PlayerData.Instance.ArmyLocalData.SquadLocalDataList[newSquadId].ModelLocalDataList.Count, PlayerData.Instance.SquadMaxCapacity);

			_canModelCountToMove = Mathf.Max(
				0,
				Mathf.Min(
					PlayerData.Instance.GetModelsCount(squadId, modelType),
					PlayerData.Instance.SquadMaxCapacity - PlayerData.Instance.GetModelsCount(squadId)
				)
			);
		}

		public void Accept()
		{
			ArmyManagementWindow.Instance.DataMoveModelsTo(_fromSquadId, _toSquad, _modelType, _resultModelCountToMove);
			SetVisible(false);
		}

		public void Cancel()
		{
			SetVisible(false);
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1f : 0f;
			_canvasGroup.blocksRaycasts = value;
		}

		public void BarValueChanged(float value)
		{
			_resultModelCountToMove = Mathf.RoundToInt(_canModelCountToMove * value);
			_movedModelCountText.text = _resultModelCountToMove.ToString();

			_squadCapacityText1.text = string.Format(_squadCapacityTextFormat, PlayerData.Instance.ArmyLocalData.SquadLocalDataList[_fromSquadId].ModelLocalDataList.Count - _resultModelCountToMove, PlayerData.Instance.SquadMaxCapacity);
			_squadCapacityText2.text = string.Format(_squadCapacityTextFormat, PlayerData.Instance.ArmyLocalData.SquadLocalDataList[_toSquad].ModelLocalDataList.Count + _resultModelCountToMove, PlayerData.Instance.SquadMaxCapacity);
		}
	}
}