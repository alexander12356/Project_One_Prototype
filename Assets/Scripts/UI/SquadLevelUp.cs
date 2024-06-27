using System.Collections.Generic;
using Mech.Data.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquadLevelUp : MonoBehaviour
{
	public ModelGlobalDataList ModelGlobalDataList;
	public Image Icon;
	public TMP_Text _modelNameText;
	public TMP_Text _modelCountText;
	public TMP_Text _levelUpModelCountText;
	public string _modelCountTextFormat;
	public string _levelUpModelCountTextFormat;

	private bool _isLevelUp;

	public void SetStat(KeyValuePair<ModelType, LocalBattleUi.ModelStat> modelStat)
	{
		Icon.sprite = ModelGlobalDataList.GetModelData(modelStat.Key).Icon;
		_modelNameText.text = modelStat.Key.ToString();
		_modelCountText.text = string.Format(_modelCountTextFormat, modelStat.Value.Count);
		_levelUpModelCountText.text = string.Format(_levelUpModelCountTextFormat, modelStat.Value.LevelUpCount);
	}
}