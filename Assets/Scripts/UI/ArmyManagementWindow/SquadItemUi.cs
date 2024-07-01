using Mech.Data.Local;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mech.UI
{
	public class SquadItemUi : MonoBehaviour
	{
		public CanvasGroup CanvasGroup;
		public int SquadId;
		public Transform ModelListHolder;
		public TMP_Text _titleText;
		public TMP_Text _modelCapacityText;
		public string _titleTextFormat;
		public string _modelCapacityTextFormat;

		public void AddModel(ModelItemUi modelItem)
		{
			modelItem.transform.SetParent(ModelListHolder, false);
			_titleText.text = string.Format(_titleTextFormat, SquadId);
			_modelCapacityText.text = string.Format(_modelCapacityTextFormat, ModelListHolder.childCount, PlayerData.Instance.SquadMaxCapacity);
		}

		public void Clear()
		{
			for (int i = 0; i < ModelListHolder.childCount; i++)
			{
				Destroy(ModelListHolder.GetChild(i).gameObject);
			}
		}

		public void SetRaycast(bool value)
		{
			CanvasGroup.blocksRaycasts = value;
		}
	}
}