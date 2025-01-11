using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {
    public Slider LoadingSlider;
    public IEnumerator LoadLevelAsync(string level){
        AsyncOperation loadOperation =(AsyncOperation)LoadLevelAsync(level);
        while (!loadOperation.isDone){
            float progress=Mathf.Clamp01(loadOperation.progress/0.9f);
            LoadingSlider.value=progress;
            yield return null;
        }
    }
}