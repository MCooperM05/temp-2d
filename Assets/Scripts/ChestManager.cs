using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.Controls;
using NUnit.Framework;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private Sprite openedChestSprite;
    [SerializeField] private Sprite closedChestSprite;
    [SerializeField] private GameObject lockedPrompt;
    [SerializeField] private GameObject lootedPrompt;
    [SerializeField] private GameObject giveHeart; // give extra heart when player opens chest
    [SerializeField] private float extraHealthAmount = 1f;
    [SerializeField] private TMP_Text lockedText;
    [SerializeField] private TMP_Text lootedText;
    [SerializeField] private int requiredKeyAmount; // Chests can have more than 1 key needed to open

    public int keys;
    public bool isOpened = false;
    public bool isUnlockable = false;
    private bool isInteractable = false;
    private GameObject player;

    public static bool hasLooted = false;

    void Start()
    {
        if (requiredKeyAmount == 1) // change text based on required amount of keys
        {
            lockedText.text = "Need " + requiredKeyAmount + " key to unlock.";
        } else
        {
            lockedText.text = "Needs " + requiredKeyAmount + " keys to unlock.";
        }

        player = GameObject.FindGameObjectWithTag("Player"); // find the player at the start for later use
        if (player != null)
        {
            Debug.Log("Player found!");
        }

        if (hasLooted)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            lootedText.text = "Already looted.";
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteractable = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteractable = false;
        }
    }

    void Update()
    {
        keys = PlayerStats.totalKeys;
        if (keys >= requiredKeyAmount)
        {
            isUnlockable = true;
        }

        if (!isOpened && isUnlockable && isInteractable && Keyboard.current.eKey.wasPressedThisFrame) // subtract key amount from player
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            player.GetComponent<PlayerStats>().UpdateTotalKeys(-requiredKeyAmount);
            giveExtraHealth();
            isOpened = true; // chest has been opened and cannot be messed with
            hasLooted = true;
        } else if (!isOpened && !isUnlockable && isInteractable && Keyboard.current.eKey.wasPressedThisFrame) // show amount of keys needed to unlock
        {
            StartCoroutine(ShowLockedText());
        }
        else if (hasLooted && isInteractable && Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(ShowLootedText());
        }
    }

    IEnumerator ShowLockedText()
    {
        lockedPrompt.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        lockedPrompt.SetActive(false);
    }

    IEnumerator ShowLootedText()
    {
        lootedPrompt.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        lootedPrompt.SetActive(false);
    }

    public void giveExtraHealth()
    {
        player.GetComponent<PlayerStats>().IncreaseHealth(extraHealthAmount); // reset player health to full
    }
}
