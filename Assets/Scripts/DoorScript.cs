using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [Tooltip("Name of the next level/scene to load")]
    public string nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the door
        if (collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevel))
        {
            // Load the next scene using its name
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("Next level name is not set in the DoorTrigger script.");
        }
    }
}
