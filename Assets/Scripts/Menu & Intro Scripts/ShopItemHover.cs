using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class ShopItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(2, 4)] public string description;
    public TMP_Text descriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionText == null) return;

        ShopItem item = GetComponentInParent<ShopItem>();
        if (item != null)
        {
            descriptionText.text = item.itemName + " - " + item.itemPrice + " gold\n" + description;
        }
        else
        {
            descriptionText.text = description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionText != null) descriptionText.text = "";
    }

    void OnDisable()
    {
        if (descriptionText != null) descriptionText.text = "";
    }
}
