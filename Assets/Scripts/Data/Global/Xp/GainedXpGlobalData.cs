using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/GainedXpGlobalData", fileName = nameof(GainedXpGlobalData))]
	public class GainedXpGlobalData : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<ModelType, int> _gainedXp;

		public int GetXpFor(ModelType type)
		{
			return _gainedXp[type];
		}
	}
}