using System.Collections.Generic;
using Mech.Data.LocalData;
using UnityEngine;

namespace LocalBattle3d
{
	public class SquadObject : MonoBehaviour
	{
		public ModelObject ModelPrefab;
		private ModelLocalData[,] modelsHolders;
		public List<ModelObject> ModelList;

		public void CreateModels(SquadLocalData squadLocalData)
		{
			var modelCount = squadLocalData.ModelLocalDataList.Count;
			modelsHolders = new ModelLocalData[4, 25];

			var createdModelCount = 0;
			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (createdModelCount >= modelCount)
					{
						break;
					}

					modelsHolders[i, j] = squadLocalData.ModelLocalDataList[createdModelCount];
					createdModelCount++;
				}
			}

			ModelList = new List<ModelObject>();

			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (modelsHolders[i, j] == null)
					{
						continue;
					}

					var modelObject = Instantiate(ModelPrefab, transform);
					modelObject.SetPosition(j * LocalBattleControllerData.Instance.ColumnOffset, i * LocalBattleControllerData.Instance.RowOffset);
					modelObject.SetData(modelsHolders[i, j]);
					ModelList.Add(modelObject);
				}
			}
		}

		public void ModelsMove(float moveForwardDistance)
		{
			foreach (var modelObject in ModelList)
			{
				modelObject.Move(moveForwardDistance);
			}
		}

		public void Charge()
		{
			foreach (var modelObject in ModelList)
			{
				modelObject.Charge();
			}
		}

		public void ReturnToStartPositions()
		{
			foreach (var modelObject in ModelList)
			{
				modelObject.ReturnToStartPositions();
			}
		}

		public void Clear()
		{
			foreach (var modelObject in ModelList)
			{
				Destroy(modelObject.gameObject);
			}
		}
	}
}