using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    [SerializeField] public int keyValue = 1;
    [SerializeField] public bool isBossKey = false;

    void Awake (){
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has this many keys: " + PlayerStats.totalKeys);
            other.gameObject.GetComponent<PlayerStats>().UpdateTotalKeys(keyValue);
            Debug.Log("Player has this many keys: " + PlayerStats.totalKeys);
            if (isBossKey) BossManager.keyCollected = true;
            Destroy(gameObject);
        }
    }
}
