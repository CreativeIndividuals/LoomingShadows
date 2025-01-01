using UnityEngine;
using UnityEngine.UI;

public class IDialogueManager : MonoBehaviour
{
    public static IDialogueManager Instance;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public KeyCode continueKey = KeyCode.Space;

    private string[] dialogueLines;
    private int currentLineIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(continueKey))
        {
            currentLineIndex++;
            ShowCurrentLine();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueLines = null;
        currentLineIndex = 0;
    }
}
