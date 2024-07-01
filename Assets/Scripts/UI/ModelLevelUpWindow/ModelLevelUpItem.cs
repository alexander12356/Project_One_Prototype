using System;
using Mech.Data.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class ModelLevelUpItem : MonoBehaviour
	{
		[SerializeField] private RectTransform _childRectTransform;
		[SerializeField] private ModelGlobalDataList _modelGlobalDataList;
		[SerializeField] private Image _iconImage;
		[SerializeField] private TMP_Text _nameText;

		private ModelType _modelType;
		private Action<ModelType> _modelSelectHandler;
		
		public void Init(ModelType modelType, Action<ModelType> modelSelectHandler)
		{
			_modelType = modelType;
			_modelSelectHandler = modelSelectHandler;
			_nameText.text = _modelGlobalDataList.GetModelData(modelType).Title;
			_iconImage.sprite = _modelGlobalDataList.GetModelData(modelType).Icon;
		}

		public void ModelSelect()
		{
			_modelSelectHandler?.Invoke(_modelType);
		}

		public RectTransform ChildTransform => _childRectTransform;

		public Transform GetLineConnectorTarget()
		{
			return _iconImage.transform;
		}
	}
}