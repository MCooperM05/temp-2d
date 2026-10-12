using UnityEngine;

public class UseShopUI : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void BuyItem(GameObject item)
    {
        ShopItem itemData = item.GetComponent<ShopItem>();
        PlayerHealthAndGold playerGold = player.GetComponent<PlayerHealthAndGold>();

        if (playerGold.totalGold >= itemData.itemPrice)
        {
            item.SetActive(false);
            playerGold.totalGold -= itemData.itemPrice;
            playerGold.UpdateTotalGold(playerGold.totalGold);
            
            Debug.Log("Item purchased: " + itemData.itemName);
        }
        else
        {
            Debug.Log("Insufficient funds for: " + itemData.itemName);
        }
    }
}
