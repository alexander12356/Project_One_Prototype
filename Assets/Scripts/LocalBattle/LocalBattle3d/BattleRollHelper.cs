using UnityEngine;

namespace LocalBattle3d
{
	public static class BattleRollHelper
	{
		public static bool Roll(int targetRoll)
		{
			var rollValue = Random.Range(0, 7);
			return rollValue >= targetRoll;
		}

		public static bool WoundRoll(int attackerS, int defenderT)
		{
			var targetRoll = 6;
			if (attackerS / 2 > defenderT)
			{
				targetRoll = 2;
			}
			else if (attackerS > defenderT)
			{
				targetRoll = 3;
			}
			else if (attackerS == defenderT)
			{
				targetRoll = 4;
			}
			else if (attackerS / 2 < defenderT)
			{
				targetRoll = 6;
			}
			else
			{
				targetRoll = 5;
			}

			return Roll(targetRoll);
		}
	}
}