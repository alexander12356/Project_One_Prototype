using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SquadLevelUp : MonoBehaviour
{
	public Image ExpBar;
	public float ExpBarFillDuration;
	public Image Icon;
	public GameObject LevelUpView;

	private bool _isLevelUp;
	
	public void SetExp(int currentExp, int squadObjectGettedExp, int maxExp)
	{
		ExpBar.fillAmount = (float)currentExp / maxExp;
		ExpBar
			.DOFillAmount((float)(squadObjectGettedExp + currentExp) / maxExp, ExpBarFillDuration)
			.OnComplete(() =>
			{
				if (_isLevelUp) LevelUpView.SetActive(true);
			});
	}

	public void SetIcon(Sprite icon)
	{
		Icon.sprite = icon;
	}

	public void SetLevelUp(bool value)
	{
		_isLevelUp = true;
	}
}