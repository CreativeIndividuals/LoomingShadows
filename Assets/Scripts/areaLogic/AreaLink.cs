using UnityEngine;
using UnityEngine.UI;

public class AreaLink : MonoBehaviour
{
    [Tooltip("Name of the next level/scene to load")]
    public string nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))return;
        if (string.IsNullOrEmpty(nextLevel))return;
        SceneLoader.instance.LoadScene(nextLevel);
    }

}
