using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
	public TMP_Text CountText;
	
	public void SetCount(int count)
	{
		CountText.text = count.ToString();
	}
}