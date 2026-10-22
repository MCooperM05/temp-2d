using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public GameObject crabPrefab;
    [SerializeField] public PlayerStats playerStats;
    private int totalEnemies;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("LittleEnemy");
        totalEnemies = enemies.Length;

        if (totalEnemies < 2) {
            Vector3 spawnPosition = new Vector3(3f, -2f, 0f);
            GameObject crab = Instantiate(crabPrefab, spawnPosition, Quaternion.identity);
            EnemyHealth enemyHealth = crab.GetComponent<EnemyHealth>();
            enemyHealth.playerStats = FindFirstObjectByType<PlayerStats>();
        }
        totalEnemies = 0;
    }
}
