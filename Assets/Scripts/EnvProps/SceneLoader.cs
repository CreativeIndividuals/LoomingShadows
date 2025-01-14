using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {
    public static SceneLoader instance;
    private void Awake() {
        if (instance!=null && instance!=this)
        {
            Destroy(this);
        }else{
            instance=this;
        }
    }
    public Slider LoadingSlider;
    [SerializeField]Animator animator;
    public void LoadScene(string level){
        Debug.Log("loading next level");
        StartCoroutine(LoadLevelAsync(level));
    }
    public IEnumerator LoadLevelAsync(string level){
        animator.SetTrigger("fadeout");
        yield return new WaitForSeconds(1f);//wait for fadeout animation
        LoadingSlider.gameObject.SetActive(true);
        AsyncOperation loadOperation =SceneManager.LoadSceneAsync(level);
        while (!loadOperation.isDone){
            LoadingSlider.value=Mathf.Clamp01(loadOperation.progress/0.9f);
            yield return null;
        }
    }
}