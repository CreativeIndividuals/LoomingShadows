using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] ItemData[] Items;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] GameObject WorldItemPrefab;
    [Space]
    [SerializeField] InventorySlot[] Slots;

    [SerializeField] List<Item> CurrentItems;

    public void AddItem(string id, int amount)
    {
        ItemData data = Items[0];
        for (int i = 0; i < Items.Length; i++){
            if (Items[i].Id == id){
                data = Items[i];
                break;
            }   
        }

        Instantiate(ItemPrefab, transform.position, Quaternion.identity).TryGetComponent(out Item item);
        
        item.itemData = data;
        item.amount = amount;

        CheckForExcess(item, item.itemData.MaxStack);

        PlaceOnSlot(item);
    }

    void PlaceOnSlot(Item item)
    {
        bool foundSlot = false;
        //check for similar item to merge
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].transform.childCount == 1)
            {
                Item slotItem = Slots[i].transform.GetComponentInChildren<Item>();
                if (slotItem.amount < slotItem.itemData.MaxStack && slotItem.itemData.Id == item.itemData.Id)
                {
                    slotItem.amount += item.amount;
                    CheckForExcess(slotItem, slotItem.itemData.MaxStack);
                    Destroy(item.gameObject);

                    foundSlot = true;
                    break;
                }
            }
        }

        if (foundSlot)
            return;

        //check for empty Slots
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].transform.childCount == 0)
            {
                item.transform.position = Slots[i].transform.position;
                item.transform.SetParent(Slots[i].transform);

                foundSlot = true;
                break;
            }
        }

        if (!foundSlot)
        {
            SpawnWorldItem(item.itemData, item.amount, GameState.instance.gameObject.transform.position);
            Destroy(item.gameObject);
        } 
    }

    void CheckForExcess(Item item, int maxStack)
    {
        if (item.amount > maxStack)
        {
            int excessAmount = item.amount - maxStack;
            item.amount = maxStack;
            AddItem(item.itemData.Id, excessAmount);
        }
    }

    public void SpawnWorldItem(ItemData data, int amount, Vector2 spawnPosition)
    {
        Instantiate(WorldItemPrefab, spawnPosition, Quaternion.identity).TryGetComponent(out WorldItem item);
        item.itemData = data;
        item.Amount = amount;

        if (item.transform.TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f) * 5f), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-80f, 80));
        }
    }

    [ContextMenu("Add Stick")]
    void AddStick()
    {
        AddItem("Stick", 1);
    }

    [ContextMenu("Add Iron")]
    void AddIron()
    {
        AddItem("Iron", 1);
    }

    [ContextMenu("Add A Lot Of Stick")]
    void AddALotOfSticks()
    {
        AddItem("Stick", 100);
    }
}
