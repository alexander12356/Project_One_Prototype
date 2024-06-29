using Mech.Data.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class ModelUpgradeItem : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private Button _upgradeButton;
		[SerializeField] private TMP_Text _title;

		private ModelType _modelType;

		public void Init(ModelGlobalData modelGlobalData)
		{
			_icon.sprite = modelGlobalData.Icon;
			_modelType = modelGlobalData.ModelType;
			_title.text = modelGlobalData.Title;
		}

		public void Upgrade()
		{
			ModelUpgradeWindow.Instance.Upgrade(_modelType);
		}

		public void SetActive(bool value)
		{
			_upgradeButton.interactable = value;
		}
	}
}