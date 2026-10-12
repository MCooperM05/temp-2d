using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            shopUI.SetActive(false);
        }
    }
}
