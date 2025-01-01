using UnityEngine;

public class NPC : Interactable
{
    [TextArea] public string[] dialogueLines;

    public override void Interact()
    {
        IDialogueManager.Instance.StartDialogue(dialogueLines);
    }
}
