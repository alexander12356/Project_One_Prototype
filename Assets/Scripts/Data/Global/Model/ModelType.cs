using System;

namespace Mech.Data.Global
{
	[Serializable]
	public enum ModelType
	{
		None = 0,
		RestoredLineMech = 1,
		BaseLineMech,
		VeteranLineMech,
		BaseHeavySupportMech,
		VeteranHeavySupportMech,
		BaseTerminatorMech,
		VeteranTerminatorMech,
		RestoredChampionMech,
		BaseChampionMech,
		VeteranChampionMech
	}
}