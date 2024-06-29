using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/UpgradeModelGlobalDataList", fileName = nameof(UpgradeModelGlobalDataList))]
	public class UpgradeModelGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<ModelType, ModelType[]> _upgradeDataList;

		public ModelType[] GetUpgrades(ModelType modelType)
		{
			return _upgradeDataList[modelType];
		}
	}
}