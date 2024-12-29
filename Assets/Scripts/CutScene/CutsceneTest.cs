using UnityEngine;
using System.Collections.Generic;

public class CutsceneTest : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public Sprite heroImage;
    public Sprite villainImage;

    void Start()
    {
        List<ICutsceneAction> actions = new List<ICutsceneAction>
        {
            new DialogueAction("Hero", "This is the beginning of the story.", heroImage, dialogueUI),
            new DialogueAction("Villain", "Prepare to face me!", villainImage, dialogueUI)
        };

        CutsceneManager cutsceneManager = gameObject.AddComponent<CutsceneManager>();
        cutsceneManager.StartCutscene(actions);
    }
}