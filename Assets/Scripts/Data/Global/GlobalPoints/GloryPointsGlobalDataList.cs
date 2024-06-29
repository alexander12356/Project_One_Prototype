using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/GloryPointsGlobalDataList", fileName = nameof(GloryPointsGlobalDataList))]
	public class GloryPointsGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<ModelType, int> _gloryPoints;

		public int GetGloryPoints(ModelType modelType)
		{
			return _gloryPoints[modelType];
		}
	}
}