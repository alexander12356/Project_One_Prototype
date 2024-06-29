using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using Mech.Data.LocalData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class ModelItemUi : MonoBehaviour
	{
		[SerializeField] private Image _modelIcon;
		[SerializeField] private TMP_Text _titleText;
		[SerializeField] private GameObject _levelUpButton;
		[SerializeField] private TMP_Text _countText;
		[SerializeField] private string _countTextFormat;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;

		private ArmyLocalData _armyLocalData;
		private List<ModelLocalData> _modelLocalDataList;
		private int _squadId;
		private ModelType _modelType;

		public void Init(int squadId, ModelType modelType, List<ModelLocalData> modelDataList)
		{
			var modelGlobalData = _modelGlobalDataList.GetModelData(modelType);
			_modelLocalDataList = modelDataList;
			_modelIcon.sprite = modelGlobalData.Icon;
			_titleText.text = modelGlobalData.Title;
			_countText.text = string.Format(_countTextFormat, modelDataList.Count);
			_squadId = squadId;
			_levelUpButton.SetActive(modelDataList.Any(x => x.IsLevelUp));
		}

		public void OpenModelUpgradeWindow()
		{
			ModelUpgradeWindow.Instance.Open(_squadId, _modelType, _modelLocalDataList.Count(x => x.IsLevelUp));
		}
	}
}