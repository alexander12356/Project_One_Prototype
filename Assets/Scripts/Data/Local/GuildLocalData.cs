using System;
using Mech.Data.Global;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.LocalData
{
	[Serializable]
	public class GuildLocalData
	{
		[SerializeField] private SerializedDictionary<ModelType, int> _models;

		public SerializedDictionary<ModelType, int> GetGuildItemLocalDataList()
		{
			return _models;
		}

		public void RemoveItem(ModelType modelType)
		{
			_models[modelType]--;
		}

		public void Clear()
		{
			_models.Clear();
		}
	}
}