using System.Collections.Generic;
using Mech.Data.Global;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Mech.UI
{
	public class ModelLevelUpWindow : MonoBehaviour
	{
		public static ModelLevelUpWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;
		[SerializeField] private WeaponGlobalDataList _weaponGlobalDataList;
		[SerializeField] private ModelLevelUpItem _modelLevelUpItem;
		[SerializeField] private FractionGlobalDataList _fractionGlobalDataList;
		[SerializeField] private RectTransform _treeHolder;
		[SerializeField] private UILineConnector _uiLineConnectorPrefab;
		[SerializeField] private LineConnectorTarget _lineConnectorTargetPrefab;
		[SerializeField] private RectTransform _lineConnectorHolders;
		[SerializeField] private Image _iconImage;
		[SerializeField] private TMP_Text _nameText;
		[SerializeField] private string _nameTextFormat;
		[SerializeField] private TMP_Text _fractionNameText;
		[SerializeField] private string _fractionNameTextFormat;
		[SerializeField] private TMP_Text _descriptionText;
		[SerializeField] private string _descriptionTextFormat;
		[SerializeField] private TMP_Text _statsText;
		[SerializeField, Multiline] private string _statsTextFormat;

		private Dictionary<ModelType, ModelLevelUpItem> _models = new ();
		private Dictionary<ModelType, LineConnectorTarget> _modelsLineConnectorTargets = new();

		private void Awake()
		{
			Instance = this;
		}

		public void Open(FractionType fractionType, ModelType modelType)
		{
			Open(fractionType);
			ModelSelectHandler(modelType);
		}

		[Button]
		public void Open(FractionType fractionType)
		{
			SetVisible(true);
			CreateTree();
			LayoutRebuilder.ForceRebuildLayoutImmediate(_treeHolder);
			return;

			void CreateTree()
			{
				var modelLevelUpGlobalDataList = _fractionGlobalDataList.GetFractionGlobalData(fractionType).ModelLevelUpGlobalDataList;

				CreateItem(ModelType.None, modelLevelUpGlobalDataList.GetFirstModel());

				foreach (var modelType in modelLevelUpGlobalDataList.NextModelDataList.Keys)
				{
					foreach (var nextModelType in modelLevelUpGlobalDataList.GetUpgrades(modelType))
					{
						CreateItem(modelType, nextModelType);
					}
				}
			}
			
			void CreateItem(ModelType parentModel, ModelType childModel)
			{
				var parentRectTransform = _models.ContainsKey(parentModel) ? _models[parentModel].ChildTransform : _treeHolder;
				var modelLevelUpItem = Instantiate(_modelLevelUpItem, parentRectTransform);
				modelLevelUpItem.Init(childModel, ModelSelectHandler);
				LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
				_models.Add(childModel, modelLevelUpItem);

				var lineConnectorTarget = Instantiate(_lineConnectorTargetPrefab, _lineConnectorHolders);
				lineConnectorTarget.SetTarget(_models[childModel].GetLineConnectorTarget());
				_modelsLineConnectorTargets.Add(childModel, lineConnectorTarget);

				if (parentRectTransform != _treeHolder)
				{
					var lineConnector = Instantiate(_uiLineConnectorPrefab, _lineConnectorHolders);
					lineConnector.transforms = new[]
					{
						_modelsLineConnectorTargets[parentModel].GetComponent<RectTransform>(), 
						_modelsLineConnectorTargets[childModel].GetComponent<RectTransform>()
					};
				}
			}
		}

		public void Close()
		{
			SetVisible(false);
			_models.Clear();
			_modelsLineConnectorTargets.Clear();
			Destroy(_treeHolder.GetChild(0).gameObject);
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1f : 0f;
			_canvasGroup.blocksRaycasts = value;
		}

		private void ModelSelectHandler(ModelType modelType)
		{
			var modelGlobalData = _modelGlobalDataList.GetModelData(modelType);
			var modelMeleeWeaponData = _weaponGlobalDataList.GetMeleeWeaponData(modelGlobalData.ModelMeleeWeaponData.Type);
			var modelRangeWeaponData = _weaponGlobalDataList.GetRangeWeaponData(modelGlobalData.ModelRangeWeaponData.Type);
			_iconImage.sprite = modelGlobalData.Icon;
			_nameText.text = string.Format(_nameTextFormat, modelGlobalData.Title);
			_fractionNameText.text = string.Format(_fractionNameTextFormat, FractionType.Mercenary);
			_descriptionText.text = string.Format(_descriptionTextFormat, modelGlobalData.Description);
			_statsText.text = string.Format(_statsTextFormat,
				modelGlobalData.T,
				modelGlobalData.Sv,
				modelGlobalData.W,
				modelMeleeWeaponData.Title,
				modelGlobalData.ModelMeleeWeaponData.WS,
				modelGlobalData.ModelMeleeWeaponData.A,
				modelMeleeWeaponData.S,
				modelMeleeWeaponData.D,
				modelRangeWeaponData.Title,
				modelGlobalData.ModelRangeWeaponData.BS,
				modelGlobalData.ModelRangeWeaponData.A,
				modelRangeWeaponData.S,
				modelRangeWeaponData.D
			);
		}
	}
}