using System.Collections.Generic;
using Mech.Data.Global;
using TMPro;
using UnityEngine;

namespace Mech.UI
{
	public class ModelUpgradeWindow : MonoBehaviour
	{
		public static ModelUpgradeWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private RectTransform _upgradeModelListHolder;
		[SerializeField] private ModelUpgradeItem _currentUpgradeModel;
		[SerializeField] private ModelUpgradeItem _modelUpgradeIconPrefab;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;
		[SerializeField] private UpgradeModelGlobalDataList _upgradeModelGlobalDataList;
		[SerializeField] private TMP_Text _upgradeCountText;
		[SerializeField] private string _upgradeCountTextFormat;

		private int _readyToUpgradeCount;
		private int _squadId;
		private ModelType _currentModelType;
		private List<ModelType> _newModels = new();
		private List<ModelUpgradeItem> _modelUpgradeItems = new ();
		

		private void Awake()
		{
			Instance = this;
		}

		public void Open(int squadId, ModelType currentModelType, int readyToUpgradeCount)
		{
			SetVisible(true);

			_squadId = squadId;
			_readyToUpgradeCount = readyToUpgradeCount;
			_currentModelType = currentModelType;

			_upgradeCountText.text = string.Format(_upgradeCountTextFormat, readyToUpgradeCount);

			_currentUpgradeModel.Init(_modelGlobalDataList.GetModelData(currentModelType));
			_currentUpgradeModel.SetActive(false);

			foreach (var modelType in _upgradeModelGlobalDataList.GetUpgrades(currentModelType))
			{
				var upgradeModel = Instantiate(_modelUpgradeIconPrefab, _upgradeModelListHolder);
				upgradeModel.Init(_modelGlobalDataList.GetModelData(modelType));
				_modelUpgradeItems.Add(upgradeModel);
			}
		}

		public void Upgrade(ModelType modelType)
		{
			_readyToUpgradeCount--;
			_newModels.Add(modelType);

			if (_readyToUpgradeCount == 0)
			{
				_modelUpgradeItems.ForEach(x => x.SetActive(false));
			}
		}

		public void Accept()
		{
			ArmyManagementWindow.Instance.UpgradeModel(_squadId, _currentModelType, _newModels);
			Close();
		}

		public void Cancel()
		{
			Close();
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