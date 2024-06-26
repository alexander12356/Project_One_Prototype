using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Mech.Data.Global
{
	[CreateAssetMenu(fileName = nameof(WeaponGlobalDataList), menuName = "Mech/Data/RangeWeaponGlobalDataList")]
	public class WeaponGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<RangeWeaponType, WeaponGlobalData> _rangeWeaponDataList;
		[SerializeField] private SerializedDictionary<MeleeWeaponType, WeaponGlobalData> _meleeWeaponDataList;

		public WeaponGlobalData GetRangeWeaponData(RangeWeaponType type)
		{
			return _rangeWeaponDataList[type];
		}

		public WeaponGlobalData GetMeleeWeaponData(MeleeWeaponType type)
		{
			return _meleeWeaponDataList[type];
		}
	}
}