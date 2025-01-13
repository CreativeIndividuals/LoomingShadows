using UnityEngine;

public class LegacyMonsterSpawner : MonoBehaviour
{
    public GameObject shadowMonsterPrefab; // Prefab to spawn
    public int monsterCount = 5; // Number of monsters to spawn
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Spawn range

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < monsterCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );
            Instantiate(shadowMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

