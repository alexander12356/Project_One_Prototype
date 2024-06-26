using System;
using UnityEngine;

namespace Mech.Data.Global
{
	[Serializable]
	public struct ModelGlobalData
	{
		public ModelType ModelType;
		public Sprite Icon;
		public string Id;

		public int T;
		public int Sv;
		public int W;

		public ModelRangeWeaponData ModelRangeWeaponData;
		
		public int NeedMoney;
	}
}