using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(fileName = nameof(VfxGlobalDataList), menuName = "Mech/Data/VfxGlobalDataList")]
	public class VfxGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<VfxType, VfxGlobalData> _vfxGlobalDataList;

		public GameObject GetVfxPrefab(VfxType type)
		{
			return _vfxGlobalDataList[type].Prefab;
		}
	}
}