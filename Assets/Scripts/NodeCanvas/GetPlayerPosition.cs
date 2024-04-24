using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;
using UnityEngine.UIElements;


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