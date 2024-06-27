using System;
using System.Collections.Generic;
using Mech.Data.Global;

namespace Mech.Data.LocalData
{
	[Serializable]
	public class SquadLocalData
	{
		public List<ModelLocalData> ModelLocalDataList;

		public void AddModel(ModelType modelType)
		{
			ModelLocalDataList.Add(new ModelLocalData()
			{
				Guid = Guid.NewGuid().ToString(),
				Type = modelType,
				Xp = 0,
				IsLevelUp = false
			});
		}

		public void InitGuid()
		{
			foreach (var modelLocalData in ModelLocalDataList)
			{
				modelLocalData.InitGuid();
			}
		}

		public int GetModelCount()
		{
			return ModelLocalDataList.Count;
		}
	}
}