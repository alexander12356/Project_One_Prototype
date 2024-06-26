using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(fileName = nameof(RangeWeaponGlobalDataList), menuName = "Mech/Data/RangeWeaponGlobalDataList")]
	public class RangeWeaponGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<RangeWeaponModelType, RangeWeaponGlobalData> _weaponDataList;

		public RangeWeaponGlobalData GetWeaponData(RangeWeaponModelType type)
		{
			return _weaponDataList[type];
		}
	}
}