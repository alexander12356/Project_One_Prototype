using UnityEngine;

public class LocalBattleUi : MonoBehaviour
{
	public void Win()
	{
		GameController.Instance.CompleteLocalBattle();
	}
}