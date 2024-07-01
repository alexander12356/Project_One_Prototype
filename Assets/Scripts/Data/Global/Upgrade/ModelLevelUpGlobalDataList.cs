using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/ModelLevelUpGlobalDataList")]
	public class ModelLevelUpGlobalDataList : ScriptableObject
	{
		[SerializeField] private ModelType _firstModel;
		[SerializeField] private SerializedDictionary<ModelType, ModelLevelUpGlobalData> _nextModelDataList;

		public ModelType GetFirstModel() => _firstModel;

		public List<ModelType> GetUpgrades(ModelType modelType)
		{
			return _nextModelDataList[modelType].NextModelList;
		}

		public SerializedDictionary<ModelType, ModelLevelUpGlobalData> NextModelDataList => _nextModelDataList;
	}
}