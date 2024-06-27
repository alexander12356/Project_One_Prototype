using Mech.World;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	[Category("Movement")]
	public class CheckDistance : ConditionTask
	{
		public BBParameter<float> Distance;

		protected override string OnInit(){
			return null;
		}

		protected override bool OnCheck()
		{
			return Vector3.Distance(Player.PositionInstance, agent.transform.position) <= Distance.value;
		}
	}
}