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
		public int BS;
		public int S;
		public int T;
		public int W;
		public int A;
		public int D;
		public int Sv;
		public int NeedMoney;
	}
}