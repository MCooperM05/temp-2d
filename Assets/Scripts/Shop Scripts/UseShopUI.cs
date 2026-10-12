using UnityEngine;

public class UseShopUI : MonoBehaviour
{
    //[SerializeField] private GameObject player;

    public void BuyItem(GameObject item)
    {
        ShopItem itemData = item.GetComponent<ShopItem>();

        if (itemData.itemPrice > 0)
        {
            item.SetActive(false);
            Debug.Log("Item purchased: " + itemData.itemName);
        }
        else
        {
            Debug.Log("Insufficient funds for: " + itemData.itemName);
        }
    }
}
