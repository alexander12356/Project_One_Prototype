using System;
using System.Collections.Generic;
using Mech.Data.Global;

namespace Mech.Data.LocalData
{
	[Serializable]
	public class ArmyLocalData
	{
		public List<SquadLocalData> SquadLocalDataList;

		public void AddModel(int squadId, ModelType modelType)
		{
			SquadLocalDataList[squadId].AddModel(modelType);
		}

		public void InitGuid()
		{
			foreach (var squadLocalData in SquadLocalDataList)
			{
				squadLocalData.InitGuid();
			}
		}

		public int GetModelCount()
		{
			var result = 0;
			foreach (var squadLocalData in SquadLocalDataList)
			{
				result += squadLocalData.GetModelCount();
			}

			return result;
		}

		public int GetSalary(ModelGlobalDataList modelGlobalDataList)
		{
			var result = 0;
			foreach (var squadLocalData in SquadLocalDataList)
			{
				foreach (var modelLocalData in squadLocalData.ModelLocalDataList)
				{
					result += modelGlobalDataList.GetModelData(modelLocalData.Type).NeedMoney;
				}
			}

			return result;
		}
	}
}