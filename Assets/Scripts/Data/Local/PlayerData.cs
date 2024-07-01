using System.Linq;
using Mech.Data.Global;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mech.Data.Local
{
	public class PlayerData : MonoBehaviour
	{
		public static PlayerData Instance;

		public ArmyLocalData ArmyLocalData;
		public InventoryLocalData InventoryLocalData;
		public ModelGlobalDataList ModelGlobalDataList;

		public int Moneys;
		public int GloryPoints;
		public int SquadMaxCapacity;

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

		public int GetAllModelCount()
		{
			return ArmyLocalData.GetAllModelCount();
		}

		public int GetSalary()
		{
			return ArmyLocalData.GetSalary(ModelGlobalDataList);
		}

		public int GetModelsCount(int squadId, ModelType modelType)
		{
			return ArmyLocalData.SquadLocalDataList[squadId].ModelLocalDataList.Count(x => x.Type == modelType);
		}

		public int GetModelsCount(int squadId)
		{
			return ArmyLocalData.SquadLocalDataList[squadId].ModelLocalDataList.Count;
		}
	}
}