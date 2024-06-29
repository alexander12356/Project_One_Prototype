using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/UpgradeModelGlobalDataList", fileName = nameof(UpgradeModelGlobalDataList))]
	public class UpgradeModelGlobalDataList : ScriptableObject
	{
		[Serializable]
		public struct ModelTypeList
		{
			public List<ModelType> List;
		}
		[SerializeField] private SerializedDictionary<ModelType, ModelTypeList> _upgradeDataList;

		public List<ModelType> GetUpgrades(ModelType modelType)
		{
			return _upgradeDataList[modelType].List;
		}
	}
}