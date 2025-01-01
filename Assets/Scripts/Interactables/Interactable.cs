using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactPrompt = "Press E to interact";

    // Called when the player interacts
    public abstract void Interact();

    // Optional: Override for specific interact behavior
    public virtual string GetPromptText()
    {
        return interactPrompt;
    }
}
