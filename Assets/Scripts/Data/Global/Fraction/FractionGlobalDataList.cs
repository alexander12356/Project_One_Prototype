using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/FractionGlobalDataList", fileName = nameof(FractionGlobalDataList))]
	public class FractionGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<FractionType, FractionGlobalData> _fractionGlobalDataList;

		public FractionGlobalData GetFractionGlobalData(FractionType fractionType)
		{
			return _fractionGlobalDataList[fractionType];
		}
	}
}