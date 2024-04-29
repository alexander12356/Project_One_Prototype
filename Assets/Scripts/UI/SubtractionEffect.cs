using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SubtractionEffect : MonoBehaviour
{
	public TMP_Text Text;

	private void Start()
	{
		Text.alpha = 1f;
		Text.transform.localPosition = Vector3.zero;
		Text.transform.DOLocalMoveY(50, 0.5f);
		Text.DOFade(0, 1.5f);
	}

	public void SetValue(int squadNeedSupplies)
	{
		Text.text = $"-{squadNeedSupplies}";
	}
}