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
		
		public void Init(ModelType childModel)
		{
			_nameText.text = _modelGlobalDataList.GetModelData(childModel).Title;
			_iconImage.sprite = _modelGlobalDataList.GetModelData(childModel).Icon;
		}

		public RectTransform ChildTransform => _childRectTransform;

		public Transform GetLineConnectorTarget()
		{
			return _iconImage.transform;
		}
	}
}