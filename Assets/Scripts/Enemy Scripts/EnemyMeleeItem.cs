using UnityEngine;

public class EnemyMeleeItem : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
