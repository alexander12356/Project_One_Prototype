using UnityEngine;

namespace LocalBattle3d
{
	public class SquadObject : MonoBehaviour
	{
		public ModelObject ModelPrefab;
		private int[,] modelsHolders = new int[4, 25];

		public void SetData(SquadGlobalData squadGlobalData)
		{
			var modelCount = squadGlobalData.ModelList.Count;

			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (modelCount <= 0)
					{
						break;
					}

					modelsHolders[i, j] = 1;
					modelCount--;
				}
			}

			for (var i = 0; i < 4; i++)
			{
				for (var j = 0; j < 25; j++)
				{
					if (modelsHolders[i, j] == 0)
					{
						continue;
					}

					var modelObject = Instantiate(ModelPrefab, transform);
					modelObject.SetPosition(j * LocalBattleControllerData.Instance.ColumnOffset, i * LocalBattleControllerData.Instance.RowOffset);
				}
			}
		}

		public void SetTransform(Transform squadTransform)
		{
			transform.SetParent(squadTransform, false);
		}
	}
}