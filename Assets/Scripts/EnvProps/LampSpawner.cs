using UnityEngine;

public class LampSpawner : MonoBehaviour
{
    public GameObject lampPrefab;  
    public int lampCount = 10;     
    void Start()
    {
        SpawnLampsRandomly();
    }

    void SpawnLampsRandomly()
    {
        for (int i = 0; i < lampCount; i++)
        {
  
            float randomX = Random.Range(0, Screen.width);
            float randomY = Random.Range(0, Screen.height);
         
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, Camera.main.nearClipPlane));
            spawnPosition.z = 0f; 

            GameObject lamp = Instantiate(lampPrefab, spawnPosition, Quaternion.identity);

            
            UnityEngine.Rendering.Universal.Light2D light = lamp.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
            if (light != null)
            {
                light.intensity = Random.Range(0.5f, 1.5f); // Random light intensity
            }
        }
    }
}
