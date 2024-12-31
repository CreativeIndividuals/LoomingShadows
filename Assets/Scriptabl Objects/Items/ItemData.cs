using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        CraftItem,
        Weapon
    }

    public ItemType Type;
    [Space]
    public string Id;
    public Sprite ItemSprite;
    [Space]
    public int MaxStack;
    [Space]
    bool Throwable = true;
}
