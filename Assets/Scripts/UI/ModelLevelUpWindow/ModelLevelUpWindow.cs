using System.Collections.Generic;
using Mech.Data.Global;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Mech.UI
{
	public class ModelLevelUpWindow : MonoBehaviour
	{
		public static ModelLevelUpWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private ModelLevelUpItem _modelLevelUpItem;
		[SerializeField] private FractionGlobalDataList _fractionGlobalDataList;
		[SerializeField] private RectTransform _treeHolder;
		[SerializeField] private UILineConnector _uiLineConnectorPrefab;
		[SerializeField] private LineConnectorTarget _lineConnectorTargetPrefab;
		[SerializeField] private RectTransform _lineConnectorHolders;

		private Dictionary<ModelType, ModelLevelUpItem> _models = new ();
		private Dictionary<ModelType, LineConnectorTarget> _modelsLineConnectorTargets = new();

		private void Awake()
		{
			Instance = this;
		}

		[Button]
		public void Open(FractionType fractionType)
		{
			SetVisible(true);
			CreateTree();
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
				modelLevelUpItem.Init(childModel);
				_models.Add(childModel, modelLevelUpItem);

				var lineConnectorTarget = Instantiate(_lineConnectorTargetPrefab, _lineConnectorHolders);
				lineConnectorTarget.SetTarget(_models[childModel].GetComponent<RectTransform>());
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
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1f : 0f;
			_canvasGroup.blocksRaycasts = value;
		}
	}
}