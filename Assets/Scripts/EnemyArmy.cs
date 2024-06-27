using Mech.Data.LocalData;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyArmy : MonoBehaviour
{
	[SerializeField] private ArmyLocalData _armyLocalData;

	public ArmyLocalData GetLocalArmyData => _armyLocalData;

	[Button]
	public void SetGuids()
	{
		_armyLocalData.InitGuid();
	}
}
