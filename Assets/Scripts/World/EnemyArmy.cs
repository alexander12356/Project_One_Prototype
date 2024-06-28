using Mech.Data.Global;
using Mech.Data.LocalData;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyArmy : MonoBehaviour
{
	[SerializeField] private ArmyLocalData _armyLocalData;
	[SerializeField] private DialogType _dialogType;

	public ArmyLocalData GetLocalArmyData => _armyLocalData;

	[Button]
	public void SetGuids()
	{
		_armyLocalData.InitGuid();
	}

	public DialogType GetDialogueType()
	{
		return _dialogType;
	}
}
