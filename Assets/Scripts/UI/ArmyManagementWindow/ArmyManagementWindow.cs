using System;
using System.Collections.Generic;
using System.Linq;
using Mech.Data.Global;
using Mech.Data.LocalData;
using UnityEngine;

namespace Mech.UI
{
	public class ArmyManagementWindow : MonoBehaviour
	{
		public static ArmyManagementWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private ModelItemUi _modelItemUiPrefab;
		[SerializeField] private List<SquadItemUi> _squadItemUiList;
		public RectTransform DragItemHolder;

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
					var modelItem = Instantiate(_modelItemUiPrefab);
					modelItem.Init(i, modelType, squadModels[modelType]);
					_squadItemUiList[i].AddModel(modelItem);
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
			foreach (var squadItemUi in _squadItemUiList)
			{
				squadItemUi.Clear();
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

		public void MoveModelsTo(int squadId, ModelType modelType, int newSquadId)
		{
			MoveModelToNewSquadWindow.Instance.Open(squadId, modelType, newSquadId);
		}

		public void DataMoveModelsTo(int fromSquadId, int toSquad, ModelType modelType, int count)
		{
			for (int i = 0; i < count; i++)
			{
				var fromSquadModelList = PlayerData.Instance.ArmyLocalData.SquadLocalDataList[fromSquadId].ModelLocalDataList;
				var toSquadModelList = PlayerData.Instance.ArmyLocalData.SquadLocalDataList[toSquad].ModelLocalDataList;

				var moveModelData = fromSquadModelList.FirstOrDefault(x => x.Type == modelType);
				toSquadModelList.Add(moveModelData);
				fromSquadModelList.Remove(moveModelData);
			}

			ClearSquads();
			CreateSquads();
		}

		public SquadItemUi GetSquadUiItem(int squadId)
		{
			return _squadItemUiList[squadId];
		}
	}
}