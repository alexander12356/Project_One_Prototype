using Mech.Data.Global;
using Mech.Data.LocalData;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class EnemyArmy : MonoBehaviour
{
	[SerializeField] private ArmyLocalData _armyLocalData;
	[SerializeField] private DialogType _dialogType;
	[SerializeField] private TMP_Text _titleText;
	[SerializeField] private string _titleTextFormat;

	public ArmyLocalData GetLocalArmyData => _armyLocalData;

	private void Start()
	{
		_titleText.text = string.Format(_titleTextFormat, _armyLocalData.GetAllModelCount());
	}

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
