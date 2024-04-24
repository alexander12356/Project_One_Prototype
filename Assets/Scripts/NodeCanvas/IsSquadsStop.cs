using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions
{
	[Category("World/Squads")]
	public class IsSquadsStop : ConditionTask
	{
		protected override bool OnCheck()
		{
			return GameController.Instance.IsSquadsStops;
		}
	}
}