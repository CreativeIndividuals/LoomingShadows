using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public bool WeaponSlot = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.childCount == 0)
            ItemInteractions.Instance.ClickSlot(this);
    }
}
