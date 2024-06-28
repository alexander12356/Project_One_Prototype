using System;
using System.Collections.Generic;
using Mech.Data.Global;
using NodeCanvas.DialogueTrees;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mech.UI
{
	public class DialogueWindow : MonoBehaviour
	{
		public static DialogueWindow Instance;

		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private DialogGlobalDataList _dialogGlobalDataList;
		[SerializeField] private DialogueTreeController _dialogueTreeController;
		[SerializeField] private DialogueText _dialogueTextPrefab;
		[SerializeField] private RectTransform _dialogueTextHolder;
		[SerializeField] private List<Button> _answersButton;
		[SerializeField] private Image _playerPortrait;
		[SerializeField] private Image _otherPortrait;
		[SerializeField] private TMP_Text _playerName;
		[SerializeField] private TMP_Text _otherName;
		[SerializeField] private Button _continueButton;

		private DialogType _dialogType;
		private Action<int> _selectDialogueOption;
		private Action _continueDialogAction;

		private void Awake()
		{
			Instance = this;
			Subscribe();
		}

		void OnEnable()
		{
			UnSubscribe();
			Subscribe();
		}

		void OnDisable()
		{
			UnSubscribe();
		}

		void Subscribe()
		{
			DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
		}

		void UnSubscribe()
		{
			DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
		}

		[Button]
		public void Init(DialogType type)
		{
			SetVisible(true);
			SetAnswersVisibility(false);
			_dialogType = type;
			var dialogueTree = _dialogGlobalDataList.GetDialogueTree(_dialogType);
			_playerPortrait.sprite = dialogueTree.actorParameters[0].actor.portraitSprite;
			_otherPortrait.sprite = dialogueTree.actorParameters[1].actor.portraitSprite;
			_playerName.text = dialogueTree.actorParameters[0].actor.name;
			_otherName.text = dialogueTree.actorParameters[1].actor.name;
		}

		[Button]
		public void StartDialogue()
		{
			var dialogueTree = _dialogGlobalDataList.GetDialogueTree(_dialogType);
			_dialogueTreeController.StartDialogue(dialogueTree, dialogueTree.actorParameters[0].actor, null);
		}

		private void OnSubtitlesRequest(SubtitlesRequestInfo info)
		{
			var color = info.actor.dialogueColor;
			var speechText = info.statement.text;
			var textView = Instantiate(_dialogueTextPrefab, _dialogueTextHolder);
			textView.Init(speechText, color);
			SetAnswersVisibility(false);
			_continueDialogAction = info.Continue;
			_continueButton.gameObject.SetActive(true);
		}

		private void SetAnswersVisibility(bool value)
		{
			_answersButton.ForEach(x => x.gameObject.SetActive(value));
		}

		private void OnMultipleChoiceRequest(MultipleChoiceRequestInfo info)
		{
			_selectDialogueOption = info.SelectOption;
			SetAnswersVisibility(false);
			foreach (var pair in info.options)
			{
				_answersButton[pair.Value].gameObject.SetActive(true);
				_answersButton[pair.Value].GetComponentInChildren<TMP_Text>().text = pair.Key.text;
			}
		}

		public void Continue()
		{
			_continueButton.gameObject.SetActive(false);
			_continueDialogAction?.Invoke();
			_continueDialogAction = null;
		}

		public void AnswerButtonPress(int id)
		{
			_selectDialogueOption.Invoke(id);
			_selectDialogueOption = null;
			SetAnswersVisibility(false);
		}

		public void CompleteDialogueAndFight()
		{
			SetVisible(false);
			GameController.Instance.StartLocalBattle();
		}

		private void SetVisible(bool value)
		{
			_canvasGroup.alpha = value ? 1 : 0;
			_canvasGroup.blocksRaycasts = value;
		}
	}
}