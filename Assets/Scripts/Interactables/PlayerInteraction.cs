using UnityEngine;
using TMPro; 

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    public LayerMask interactableLayer;
    public KeyCode interactionKey = KeyCode.E;

    private Interactable currentInteractable;

    
    public TextMeshProUGUI interactionPrompt; 
    

    void Update()
    {
        DetectInteractable();
        HandleInteraction();
    }

    void DetectInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

        if (hits.Length > 0)
        {
            currentInteractable = hits[0].GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                ShowPrompt(currentInteractable.GetPromptText());
            }
        }
        else
        {
            HidePrompt();
            currentInteractable = null;
        }
    }

    void HandleInteraction()
    {
        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    void ShowPrompt(string promptText)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.text = promptText;
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    void HidePrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }
}
