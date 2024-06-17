using UnityEngine;

namespace LocalBattle3d
{
	public class SquadObject : MonoBehaviour
	{
		public ModelObject ModelPrefab;
		private ModelType[,] modelsHolders = new ModelType[4, 25];

		public void SetData(SquadGlobalData squadGlobalData)
		{
			var modelCount = squadGlobalData.ModelList.Count;

			var createdModelCount = 0;
			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (createdModelCount >= modelCount)
					{
						break;
					}

					modelsHolders[i, j] = squadGlobalData.ModelList[createdModelCount];
					createdModelCount++;
				}
			}

			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (modelsHolders[i, j] == ModelType.None)
					{
						continue;
					}

					var modelObject = Instantiate(ModelPrefab, transform);
					modelObject.SetPosition(j * LocalBattleControllerData.Instance.ColumnOffset, i * LocalBattleControllerData.Instance.RowOffset);
					modelObject.SetType(modelsHolders[i, j]);
				}
			}
		}

		public void SetTransform(Transform squadTransform)
		{
			transform.SetParent(squadTransform, false);
		}
	}
}