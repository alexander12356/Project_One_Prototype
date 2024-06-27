using Mech.World;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	[Category("Player")]
	public class GetPlayerPosition : ActionTask
	{
		public BBParameter<Vector3> PlayerPosition;

		protected override void OnExecute()
		{
			PlayerPosition.value = Player.PositionInstance;
			EndAction(true);
		}
	}
}