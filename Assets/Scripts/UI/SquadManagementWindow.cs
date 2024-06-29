using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using Mech.Data.LocalData;
using UnityEngine;

namespace Mech.UI
{
	public class SquadManagementWindow : MonoBehaviour
	{
		public static SquadManagementWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private ModelItemUi _modelItemUiPrefab;
		[SerializeField] private List<RectTransform> _squadHolderList;

		public void Awake()
		{
			Instance = this;
		}

		public void Open()
		{
			SetVisible(true);
			ClearSquads();
			CreateSquads();
		}

		private void CreateSquads()
		{
			for (int i = 0; i < PlayerData.Instance.ArmyLocalData.SquadLocalDataList.Count; i++)
			{
				var squadLocalData = PlayerData.Instance.ArmyLocalData.SquadLocalDataList[i];
				var squadModels = GetModelsCountFromSquad(squadLocalData.ModelLocalDataList);

				foreach (var modelType in squadModels.Keys)
				{
					var modelItem = Instantiate(_modelItemUiPrefab, _squadHolderList[i]);
					modelItem.Init(i, modelType, squadModels[modelType]);
				}
			}

			return;

			Dictionary<ModelType, List<ModelLocalData>> GetModelsCountFromSquad(List<ModelLocalData> modelLocalDataList)
			{
				var result = new Dictionary<ModelType, List<ModelLocalData>>();
				foreach (var modelLocalData in modelLocalDataList)
				{
					var modelType = modelLocalData.Type;
					if (!result.ContainsKey(modelType))
					{
						result.Add(modelType, new List<ModelLocalData>());
					}

					result[modelType].Add(modelLocalData);
				}

				return result;
			}
		}

		private void ClearSquads()
		{
			foreach (var squadHolder in _squadHolderList)
			{
				for (int i = 0; i < squadHolder.childCount; i++)
				{
					Destroy(squadHolder.GetChild(i).gameObject);
				}
			}
		}

		public void Close()
		{
			SetVisible(false);
			GameController.Instance.ReturnFromSquadManagementWindow();
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1f : 0f;
			_canvasGroup.blocksRaycasts = value;
		}

		public void UpgradeModel(int squadId, ModelType oldModelType, List<ModelType> newModels)
		{
			for (int i = 0; i < newModels.Count; i++)
			{
				var modelList = PlayerData.Instance.ArmyLocalData.SquadLocalDataList[squadId].ModelLocalDataList;
				var modelLocalData = modelList.FirstOrDefault(x => x.Type == oldModelType && x.IsLevelUp);
				modelList.Remove(modelLocalData);
			}

			foreach (var newModelType in newModels)
			{
				var modelList = PlayerData.Instance.ArmyLocalData.SquadLocalDataList[squadId].ModelLocalDataList;
				modelList.Add(new ModelLocalData
				{
					Guid = Guid.NewGuid().ToString(),
					IsLevelUp = false,
					Type = newModelType,
					Xp = 0
				});
			}

			ClearSquads();
			CreateSquads();
		}
	}
}