using System;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private List<ICutsceneAction> actions;
    private int currentActionIndex = 0;

    public void StartCutscene(List<ICutsceneAction> cutsceneActions)
    {
        actions = cutsceneActions;
        currentActionIndex = 0;
        ExecuteNextAction();
    }

    private void ExecuteNextAction()
    {
        if (currentActionIndex < actions.Count)
        {
            actions[currentActionIndex].Execute(() =>
            {
                currentActionIndex++;
                ExecuteNextAction();
            });
        }
        else
        {
            Debug.Log("Cutscene complete!");
        }
    }
}
