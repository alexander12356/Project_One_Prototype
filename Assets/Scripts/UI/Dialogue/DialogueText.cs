using TMPro;
using UnityEngine;

namespace Mech.UI
{
	public class DialogueText : MonoBehaviour
	{
		[SerializeField] private TMP_Text _speechText;

		public void Init(string speechText, Color color)
		{
			_speechText.text = speechText;
			_speechText.color = color;
		}
	}
}