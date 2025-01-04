using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField] ItemData[] Items;
    [SerializeField] int SpawnAmount;
    [SerializeField] float Range;

    [ContextMenu("Spawn Resources")]
   void SpawnResources()
    {
        for (int i = 0; i < SpawnAmount; i++)
        {
            ItemData item = Items[Random.Range(0, Items.Length)];
            Inventory.Instance.SpawnWorldItem(item, 1, new Vector2(transform.position.x + Random.Range(-Range, Range), transform.position.y + Random.Range(-Range, Range)));
        }
    }
}
