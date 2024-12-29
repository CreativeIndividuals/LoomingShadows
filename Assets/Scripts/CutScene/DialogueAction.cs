using System;
using UnityEngine;

public class DialogueAction : ICutsceneAction
{
    private string speakerName;
    private string dialogueText;
    private Sprite cutsceneImage;
    private DialogueUI dialogueUI;

    public DialogueAction(string speaker, string text, Sprite image, DialogueUI ui)
    {
        speakerName = speaker;
        dialogueText = text;
        cutsceneImage = image;
        dialogueUI = ui;
    }

    public void Execute(Action onComplete)
    {
        dialogueUI.ShowDialogue(speakerName, dialogueText, cutsceneImage, onComplete);
    }
}
