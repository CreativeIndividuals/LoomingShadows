using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public ItemData itemData;
    public int amount;
    [Space]
    [SerializeField] TextMeshProUGUI AmountText;

    private void Start()
    {
        GetComponent<Image>().sprite = itemData.ItemSprite;
    }

    private void Update()
    {
        if (amount == 0)
            Destroy(gameObject);

        if (amount > 1)
        {
            AmountText.enabled = true;
            AmountText.text = amount.ToString();
        }
        else
            AmountText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemInteractions.Instance.RequestPickup(this);
    }
}
