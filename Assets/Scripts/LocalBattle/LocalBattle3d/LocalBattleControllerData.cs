using UnityEngine;

namespace LocalBattle3d
{
	public class LocalBattleControllerData : MonoBehaviour
	{
		public static LocalBattleControllerData Instance;

		public float ColumnOffset;
		public float RowOffset;

		private void Awake()
		{
			Instance = this;
		}
	}
}