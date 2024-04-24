using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquadStatsUi : MonoBehaviour
{
	public Image Icon;
	public string BsFormat;
	public TMP_Text Bs;
	public string SFormat;
	public TMP_Text S;
	public string TFormat;
	public TMP_Text T;
	public string WFormat;
	public TMP_Text W;
	public string AFormat;
	public TMP_Text A;
	public string DFormat;
	public TMP_Text D;
	public string SvFormat;
	public TMP_Text Sv;

	public void SetStats(BalanceController.SquadGlobalData globalData)
	{
		Icon.sprite = globalData.Icon;
		Bs.text = string.Format(BsFormat, globalData.BS);
		S.text = string.Format(SFormat, globalData.S);
		T.text = string.Format(TFormat, globalData.T);
		W.text = string.Format(WFormat, globalData.W);
		A.text = string.Format(AFormat, globalData.A);
		D.text = string.Format(DFormat, globalData.D);
		Sv.text = string.Format(SvFormat, globalData.Sv);
	}

	public void SetToughness(int squadToughness)
	{
		T.text = string.Format(TFormat, squadToughness);
	}
}