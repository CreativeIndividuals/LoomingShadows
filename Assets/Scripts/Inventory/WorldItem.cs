using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [HideInInspector] public ItemData itemData;
    public int Amount = 1;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.ItemSprite;
    }
}
