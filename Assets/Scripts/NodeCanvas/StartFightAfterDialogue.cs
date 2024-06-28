using Mech.UI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {
	public class StartFightAfterDialogue : ActionTask
	{
		protected override void OnExecute()
		{
			DialogueWindow.Instance.CompleteDialogueAndFight();
			EndAction(true);
		}
	}
}