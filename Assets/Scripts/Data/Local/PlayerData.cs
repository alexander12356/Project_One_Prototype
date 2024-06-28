using Mech.Data.Global;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mech.Data.LocalData
{
	public class PlayerData : MonoBehaviour
	{
		public ArmyLocalData ArmyLocalData;
		public ModelGlobalDataList ModelGlobalDataList;

		public static PlayerData Instance;

		public int Supplies;
		public int Moneys;
		public int GloryPoints;

		public void Awake()
		{
			Instance = this;
		}

		public void AddNewModel(int squadId, ModelType modelType)
		{
			ArmyLocalData.AddModel(squadId, modelType);
		}

		[Button]
		public void InitGuids()
		{
			ArmyLocalData.InitGuid();
		}

		public int GetModelCount()
		{
			return ArmyLocalData.GetModelCount();
		}

		public int GetSalary()
		{
			return ArmyLocalData.GetSalary(ModelGlobalDataList);
		}
	}
}