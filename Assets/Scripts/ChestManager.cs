using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private Sprite openedChestSprite;
    [SerializeField] private Sprite closedChestSprite;
    [SerializeField] private GameObject lockedPrompt;
    [SerializeField] private TMP_Text lockedText;
    [SerializeField] private int requiredKeyAmount; // Chests can have more than 1 key needed to open

    public int keys;
    public bool isUnlocked = false;
    private bool isInteractable = false;
    private GameObject player;

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
            isUnlocked = true;
        }

        if (isUnlocked && isInteractable && Keyboard.current.eKey.wasPressedThisFrame) // subtract key amount from player
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            player.GetComponent<PlayerStats>().UpdateTotalKeys(-requiredKeyAmount);
        } else if (!isUnlocked && isInteractable && Keyboard.current.eKey.wasPressedThisFrame) // show amount of keys needed to unlock
        {
            StartCoroutine(ShowLockedText());
        }
    }

    IEnumerator ShowLockedText()
    {
        lockedPrompt.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        lockedPrompt.SetActive(false);
    }
}
