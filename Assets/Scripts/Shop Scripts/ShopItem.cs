using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] public int itemPrice;
    [SerializeField] public string itemName;

    void Start()
    {
        if (UseShopUI.boughtItems.Contains(itemName))
        {
            gameObject.SetActive(false);
        }
    }

}
