using UnityEngine;
using System.Collections.Generic;

public class UseShopUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public PlayerController playerController;
    [SerializeField] private AudioSource purchaseSound;
    public static List<string> boughtItems = new List<string>();

    void Start()
    {
        if (playerController == null) playerController = FindFirstObjectByType<PlayerController>();
        if (player == null) player = playerController.gameObject;
    }

    bool TryBuy(GameObject item)
    {
        ShopItem itemData = item.GetComponent<ShopItem>();

        if (PlayerStats.totalGold >= itemData.itemPrice)
        {
            item.SetActive(false);
            boughtItems.Add(itemData.itemName);

            player.GetComponent<PlayerStats>().UpdateTotalGold(-itemData.itemPrice);

            Debug.Log("Item bought: " + itemData.itemName);
            return true;
        }

        Debug.Log("Too Poor for: " + itemData.itemName);
        return false;
    }

    public void BuyGun(GameObject item)
    {
        if (TryBuy(item))
        {
            purchaseSound.Play();
            GiveGun();
        }
        // ShopItem itemData = item.GetComponent<ShopItem>();
        // PlayerHealthAndGold playerGold = player.GetComponent<PlayerHealthAndGold>();

        // if (playerGold.totalGold >= itemData.itemPrice)
        // {
        //     item.SetActive(false);
        //     playerGold.totalGold -= itemData.itemPrice;
        //     playerGold.UpdateTotalGold(playerGold.totalGold);
            
        //     Debug.Log("Item purchased: " + itemData.itemName);
        // }
        // else
        // {
        //     Debug.Log("Insufficient funds for: " + itemData.itemName);
        // }
    }

    void GiveGun()
    {
        GunManager.coolChange += .25f;
        Debug.Log("Cooldown: " + GunManager.coolChange);
    }

    public void BuyFlippers(GameObject item)
    {
        if (TryBuy(item))
        {
            purchaseSound.Play();
            GiveFlippers();
        }
        // ShopItem itemData = item.GetComponent<ShopItem>();
        // PlayerHealthAndGold playerGold = player.GetComponent<PlayerHealthAndGold>();

        // if (playerGold.totalGold >= itemData.itemPrice)
        // {
        //     item.SetActive(false);
        //     playerGold.totalGold -= itemData.itemPrice;
        //     playerGold.UpdateTotalGold(playerGold.totalGold);

        //     GiveFlippers(playerController);
            
        //     //Debug.Log("Item purchased: " + itemData.itemName);
        // }
        // else
        // {
        //     Debug.Log("Insufficient funds for: " + itemData.itemName);
        // }
    }

    public void GiveFlippers()
    {
        PlayerController.bonusSpeed += 5.0f; // Increase the player's speed by 5.0f
        Debug.Log("Player speed increased to: " + (playerController.speed + PlayerController.bonusSpeed));
    }

    public void BuyBullets(GameObject item)
    {
        if (TryBuy(item))
        {
            purchaseSound.Play();
            GiveBullets();
        }
        // ShopItem itemData = item.GetComponent<ShopItem>();
        // // PlayerHealthAndGold playerGold = player.GetComponent<PlayerHealthAndGold>();

        // if (PlayerHealthAndGold.totalGold >= itemData.itemPrice)
        // {
        //     item.SetActive(false);
        //     PlayerHealthAndGold.totalGold -= itemData.itemPrice;
        //     PlayerHealthAndGold.UpdateTotalGold(PlayerHealthAndGold.totalGold);
            
        //     Debug.Log("Item purchased: " + itemData.itemName);
        // }
        // else
        // {
        //     Debug.Log("Insufficient funds for: " + itemData.itemName);
        // }
    }

    void GiveBullets()
    {
        GunManager.bonusDamage += 2f;
        Debug.Log("Bonus DMG: " + GunManager.bonusDamage);
    }
}
