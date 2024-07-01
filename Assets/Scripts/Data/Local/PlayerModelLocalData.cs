using System.Collections;
using System.Collections.Generic;
using Mech.Data.Global;
using UnityEngine;

namespace Mech.Data.Local
{
	public class PlayerModelLocalData
	{
		public string Guid;
		public string Id;
		public ModelType ModelType;
		public int Wound;
		public int Exp;
		public bool IsLevelUp;

		public void SetGuid(string newGuid)
		{
			Guid = newGuid;
		}
	}
}