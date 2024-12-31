using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] List<WorldItem> ItemsInRange;
    [SerializeField] GameObject IndicatorObject;
    [SerializeField] TextMeshPro AmountText;

    WorldItem pickItem;
    float maxDistance = 10000f;

    private void Update()
    {
        if (ItemsInRange.Count == 1)
        {
            pickItem = ItemsInRange[0];
        }
        else if (ItemsInRange.Count > 1)
        {
            foreach (var item in ItemsInRange)
            {
                float distance = Vector2.Distance(item.transform.position, transform.position);

                if (distance < maxDistance)
                {
                    pickItem = item;
                    maxDistance = distance;
                }
            }
            maxDistance = 10000f;
        }
        else
        {
            pickItem = null;
            maxDistance = 10000f;
        }

        if (pickItem != null)
        {
            IndicatorObject.transform.position = pickItem.transform.position;
            AmountText.text = pickItem.Amount.ToString();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Inventory.Instance.AddItem(pickItem.itemData.Id, pickItem.Amount);

                ItemsInRange.Remove(pickItem);

                Destroy(pickItem.gameObject);
                pickItem = null;

                if (ItemsInRange.Count == 0)
                    IndicatorObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            ItemsInRange.Add(collision.GetComponent<WorldItem>());
            IndicatorObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ItemsInRange.Contains(collision.GetComponent<WorldItem>()))
        {
            ItemsInRange.Remove(collision.GetComponent<WorldItem>());
            if (ItemsInRange.Count == 0)
                IndicatorObject.SetActive(false);
        }
    }
}
