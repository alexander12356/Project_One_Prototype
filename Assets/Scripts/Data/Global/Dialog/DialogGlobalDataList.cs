using NodeCanvas.DialogueTrees;
using UnityEngine;
using UnityEngine.Rendering;

namespace Mech.Data.Global
{
	[CreateAssetMenu(menuName = "Mech/Data/DialogGlobalDataList", fileName = nameof(DialogGlobalDataList))]
	public class DialogGlobalDataList : ScriptableObject
	{
		[SerializeField] private SerializedDictionary<DialogType, DialogueTree> _dialogueTreeList;

		public DialogueTree GetDialogueTree(DialogType type)
		{
			return _dialogueTreeList[type];
		}
	}
}