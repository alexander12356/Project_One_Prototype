using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
	public TMP_Text Text;
	public float ToMoveY;
	public float MoveDuration;
	public float FadeDuration;

	private void Start()
	{
		transform.DOLocalMoveY(ToMoveY, MoveDuration);
		Text.DOFade(0, FadeDuration);
	}

	public void SetText(string text)
	{
		Text.text = text;
	}
}