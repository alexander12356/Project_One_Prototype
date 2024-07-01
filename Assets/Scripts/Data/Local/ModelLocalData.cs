using System;
using Mech.Data.Global;

namespace Mech.Data.Local
{
	[Serializable]
	public class ModelLocalData
	{
		public string Guid;
		public ModelType Type;
		public int Xp;
		public bool IsLevelUp;

		public void SetGuid(string newGuid)
		{
			Guid = newGuid;
		}

		public void InitGuid()
		{
			Guid = System.Guid.NewGuid().ToString();
		}
	}
}