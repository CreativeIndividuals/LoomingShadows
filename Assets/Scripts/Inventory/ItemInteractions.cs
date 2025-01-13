using UnityEngine;
using UnityEngine.UI;

public class ItemInteractions : MonoBehaviour
{
    public static ItemInteractions Instance;
    private void Awake(){ Instance = this; }

    Item requestedItem = null;
    Item heldItem = null;

    private void Update()
    {
        if (heldItem == null)
            return;

        heldItem.transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Q) || OpenCloseInventory.Instance.InventoryObject.activeInHierarchy == false)
        {
            Inventory.Instance.SpawnWorldItem(heldItem.itemData, heldItem.amount, GameState.instance.gameObject.transform.position);
            Destroy(heldItem.gameObject);
            heldItem = null;
        }

        if (requestedItem == null)
            return;

        if (requestedItem.itemData.Id == heldItem.itemData.Id && requestedItem.amount < requestedItem.itemData.MaxStack)
        {
            int combinedAmount = requestedItem.amount + heldItem.amount;
            if (combinedAmount > requestedItem.itemData.MaxStack)
            {
                requestedItem.amount = requestedItem.itemData.MaxStack;
                heldItem.amount = combinedAmount - requestedItem.itemData.MaxStack;
                requestedItem = null;
            }
            else
            {
                requestedItem.amount += heldItem.amount;
                heldItem.amount -= heldItem.amount;
                requestedItem = null;
            }
        }
        else if (requestedItem.itemData.Id != heldItem.itemData.Id)
        {
            heldItem.transform.position = requestedItem.transform.position;
            heldItem.transform.SetParent(requestedItem.transform.parent);
            heldItem.GetComponent<Image>().raycastTarget = true;
            heldItem = null;
            
            heldItem = requestedItem;
            heldItem.transform.SetParent(transform);
            heldItem.GetComponent<Image>().raycastTarget = false;
            requestedItem = null;
        }
        else
        {
            requestedItem = null;
        }
    }

    public void RequestPickup(Item item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            heldItem.transform.SetParent(transform);
            heldItem.GetComponent<Image>().raycastTarget = false;
        }
        else if (heldItem != item)
            requestedItem = item;
    }

    public void ClickSlot(InventorySlot slot)
    {
        if (heldItem != null)
        {
            heldItem.transform.position = slot.transform.position;
            heldItem.transform.SetParent(slot.transform);
            heldItem.GetComponent<Image>().raycastTarget = true;
            heldItem = null;
        }
    }
}
