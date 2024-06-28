using System;
using UnityEngine;

namespace Mech.Data.Global
{
	[Serializable]
	public struct FocusGlobalData
	{
		public int Cost;
		public string Title;
		[Multiline] public string LoreDescription;
		[Multiline] public string StatDescription;
	}
}