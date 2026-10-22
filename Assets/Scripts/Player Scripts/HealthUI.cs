
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartTransform;
    [SerializeField] private PlayerStats playerStats;

    private GameObject[] fullHearts;
    private GameObject[] emptyHearts;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartHearts();
    }

    void Start()
    {
        StartHearts();
    }

    void StartHearts()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        playerStats = player.GetComponent<PlayerStats>();

        for (int i = heartTransform.childCount - 1; i >= 0; i--)
            Destroy(heartTransform.GetChild(i).gameObject);

        int maxHearts = Mathf.CeilToInt(playerStats.maxHealth);

        fullHearts = new GameObject[maxHearts];
        emptyHearts = new GameObject[maxHearts];

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartTransform);
            fullHearts[i] = heart.transform.GetChild(0).gameObject;
            emptyHearts[i] = heart.transform.GetChild(1).gameObject;
        }

        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (fullHearts == null) return;

        for (int i = 0; i < fullHearts.Length; i++)
        {
            fullHearts[i].SetActive(i < playerStats.health);
            emptyHearts[i].SetActive(i >= playerStats.health);
        }
    }

    public void AddHearts()
    {
        StartHearts();
    }
}
