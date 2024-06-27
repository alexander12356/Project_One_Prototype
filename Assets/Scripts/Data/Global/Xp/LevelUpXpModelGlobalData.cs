using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/LevelUpXpModelGlobalData", fileName = nameof(LevelUpXpModelGlobalData))]
	public class LevelUpXpModelGlobalData : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<ModelType, int> _levelUpXp;

		public int GetLevelUpXp(ModelType type)
		{
			return _levelUpXp[type];
		}
	}
}