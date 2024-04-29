using TMPro;
using UnityEngine;

public class City : MonoBehaviour
{
	public string CityName;
	public TMP_Text CityCaption;
	public Transform EnterPositionTransform;
	public Transform ExitPositionTransform;

	public Vector3 ExitPosition => ExitPositionTransform.position;
	public Vector3 EnterPosition => EnterPositionTransform.position;

	public void Start()
	{
		CityCaption.text = CityName;
	}
}