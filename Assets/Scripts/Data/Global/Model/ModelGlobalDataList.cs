using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Mech.Data.Global
{
	[CreateAssetMenu(fileName = nameof(ModelGlobalDataList), menuName = "Mech/Data/ModelGlobalDataList")]
	public class ModelGlobalDataList : ScriptableObject
	{
		public List<ModelGlobalData> Balances;
		public List<BalanceController.GettedExp> GettedExps;

		public ModelGlobalData GetModelData(ModelType type)
		{
			return Balances.FirstOrDefault(x => x.ModelType == type);
		}

		public int GetExpFrom(ModelType type)
		{
			return GettedExps.FirstOrDefault(x => x.ModelType == type).Exp;
		}

		public ModelRangeWeaponData GetModelRangeWeaponData(ModelType type)
		{
			return Balances.FirstOrDefault(x => x.ModelType == type).ModelRangeWeaponData;
		}

		public ModelMeleeWeaponData GetModelMeleeWeaponData(ModelType type)
		{
			return Balances.FirstOrDefault(x => x.ModelType == type).ModelMeleeWeaponData;
		}
	}
}