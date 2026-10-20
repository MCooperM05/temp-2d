using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private Sprite openedChestSprite;
    [SerializeField] private Sprite closedChestSprite;
    [SerializeField] private GameObject lockedPrompt;

    public int key = 0;
    public bool isUnlocked = false;
    private bool isInteractable = false;

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
        if (key > 0)
        {
            isUnlocked = true;
        }

        if (isUnlocked && isInteractable && Keyboard.current.eKey.wasPressedThisFrame)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedChestSprite;
        } else if (!isUnlocked && isInteractable && Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(ShowLockedText());
        }
    }

    IEnumerator ShowLockedText()
    {
        lockedPrompt.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        lockedPrompt.SetActive(false);
    }
}
