using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public Text speakerNameUI;
    public Text dialogueTextUI;
    public Image cutsceneImageUI;

    public float typingSpeed = 0.05f;

    public void ShowDialogue(string speaker, string text, Sprite cutsceneImage, Action onComplete)
    {
        speakerNameUI.text = speaker;

        if (cutsceneImageUI != null && cutsceneImage != null)
        {
            cutsceneImageUI.sprite = cutsceneImage;
            cutsceneImageUI.gameObject.SetActive(true);
        }

        StartCoroutine(TypeText(text, onComplete));
    }

    private IEnumerator TypeText(string text, Action onComplete)
    {
        dialogueTextUI.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueTextUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // Wait for user input
        onComplete?.Invoke();
    }
}
