using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField] ItemData[] Items;
    [SerializeField] int SpawnAmount;
    [SerializeField] float Range;

    private void Start()
    {
        for (int i = 0; i < SpawnAmount; i++) 
        {
            ItemData item = Items[Random.Range(0, Items.Length)];
            Inventory.Instance.SpawnWorldItem(item, 1, new Vector2(Random.Range(-Range, Range), Random.Range(-Range, Range)));
        }
    }
}
