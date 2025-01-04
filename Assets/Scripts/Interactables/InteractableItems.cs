using UnityEngine;

public class InteractableItems : Interactable
{
    [TextArea] public string lore;

    public override void Interact()
    {
        IDialogueManager.Instance.StartDialogue(new string[] { lore });
    }
}
