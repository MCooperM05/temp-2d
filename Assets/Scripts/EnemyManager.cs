using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public GameObject crabPrefab;
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public TextMeshProUGUI difficulty; 
    private int totalEnemies;
    private Vector3 spawnPosition;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("LittleEnemy");
        totalEnemies = enemies.Length;
        float timeInScene = Time.timeSinceLevelLoad;
        float rand = Random.value;

        difficulty.text = "Difficulty: " + (1 +(int)Mathf.Floor((timeInScene / 5f)));
        if (totalEnemies < 1f + (timeInScene / 5f)) { 
            if (rand > 0.75f){
                spawnPosition = new Vector3(9.44f, -11.38f, 0f);
            } else if (rand > 0.5f){
                spawnPosition = new Vector3(12.14f, -17.92f, 0f);
            } else if (rand > 0.25f) {
                spawnPosition = new Vector3(-10.82f, -21.27f, 0f);
            } else {
                spawnPosition = new Vector3(8.75f, -21.43f, 0f);
            }
            GameObject crab = Instantiate(crabPrefab, spawnPosition, Quaternion.identity);
            EnemyHealth enemyHealth = crab.GetComponent<EnemyHealth>();
            enemyHealth.playerStats = FindFirstObjectByType<PlayerStats>();
        }
        totalEnemies = 0;
    }
}
