using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {
    [SerializeField] private Slider LoadingSlider;
    IEnumerator LoadLevelAsync(String level){
        AsyncOperation loadOperation =SceneLoader.LoadSceneAsync(level);
        while (!loadOperation.isDone){
            float progress=MathF.Clamp01(loadOperation.progress/0.9f);
            LoadingSlider.value=progress;
            yield return null;
        }
    }
}