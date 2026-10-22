using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DivingStation : MonoBehaviour
{
    [Header("Where it goes")]
    public string sceneToLoad = "prototype-explore-ocean";
    public string stationID = "";
    public string arriveAtStationID = "";
    public Transform spawnPoint;

    [Header("Requirements")]
    public int keysRequired = 0;
    public bool useUpKeys = false;
    public float diveDamage = 0f;

    [Header("Prompts (optional)")]
    public GameObject pressEPrompt;
    public GameObject lockedPrompt;

    public static string nextArrivalID = "";

    bool playerNearby = false;

    void Start()
    {
        if (pressEPrompt != null)
        {
            pressEPrompt.SetActive(false);
        }
        if (lockedPrompt != null)
        {
            lockedPrompt.SetActive(false);
        }

        if (nextArrivalID != "" && nextArrivalID == stationID)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                Vector3 spot = transform.position;
                if (spawnPoint != null) spot = spawnPoint.position;
                player.transform.position = new Vector3(spot.x, spot.y, player.transform.position.z);
            }
            nextArrivalID = "";
        }
    }

    bool CanUse()
    {
        if (PlayerStats.totalKeys < keysRequired) return false;
        if (diveDamage > 0f && PlayerStats.savedHealth <= diveDamage) return false;
        return true;
    }

    void UpdatePrompts()
    {
        bool canUse = CanUse();
        if (pressEPrompt != null) pressEPrompt.SetActive(playerNearby && canUse);
        if (lockedPrompt != null) lockedPrompt.SetActive(playerNearby && !canUse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            UpdatePrompts();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            UpdatePrompts();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!playerNearby) return;

        UpdatePrompts();

        if (Keyboard.current.eKey.wasPressedThisFrame && CanUse())
        {
            if (useUpKeys) PlayerStats.totalKeys -= keysRequired;
            if (diveDamage > 0f) PlayerStats.savedHealth -= diveDamage;
            nextArrivalID = arriveAtStationID;
            SceneManager.LoadScene(sceneToLoad);
        }
        // if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        // {
        //     SceneManager.LoadScene(sceneToLoad);
        // }
    }
}
