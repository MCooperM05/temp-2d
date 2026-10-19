using UnityEngine;

public class UseShopUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public PlayerController playerController;

    public void BuyGun(GameObject item)
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

    // void GiveGun()
    // {
        
    // }

    public void BuyFlippers(GameObject item)
    {
        ShopItem itemData = item.GetComponent<ShopItem>();
        PlayerHealthAndGold playerGold = player.GetComponent<PlayerHealthAndGold>();

        if (playerGold.totalGold >= itemData.itemPrice)
        {
            item.SetActive(false);
            playerGold.totalGold -= itemData.itemPrice;
            playerGold.UpdateTotalGold(playerGold.totalGold);

            GiveFlippers(playerController);
            
            //Debug.Log("Item purchased: " + itemData.itemName);
        }
        else
        {
            Debug.Log("Insufficient funds for: " + itemData.itemName);
        }
    }

    public void GiveFlippers(PlayerController playerController)
    {
        playerController.speed += 5.0f; // Increase the player's speed by 5.0f
        Debug.Log("Player speed increased to: " + playerController.speed);
    }

    public void BuyBullets(GameObject item)
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

    // void GiveBullets()
    // {
        
    // }
}
