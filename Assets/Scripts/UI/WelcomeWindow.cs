using UnityEngine;

namespace Mech.UI
{
	public class WelcomeWindow : MonoBehaviour
	{
		public static WelcomeWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			SetVisible(true);
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1f : 0f;
			_canvasGroup.blocksRaycasts = value;
		}

		public void Close()
		{
			SetVisible(false);
		}
	}
}