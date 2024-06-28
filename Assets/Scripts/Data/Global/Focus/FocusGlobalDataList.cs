using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/FocusGlobalDataList", fileName = nameof(FocusGlobalDataList))]
	public class FocusGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<FocusType, FocusGlobalData> _dataList;

		public FocusGlobalData GetFocusGlobalData(FocusType focusType)
		{
			return _dataList[focusType];
		}
	}
}