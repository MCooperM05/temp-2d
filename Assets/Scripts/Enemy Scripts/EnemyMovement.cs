using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    [Header ("Damage Settings")]
    [SerializeField] private float damage;
    [SerializeField] private GameObject attackCollider;
    
    private GameObject player;

    void Awake()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }

    

    public void AttackEnable()
    {
        attackCollider.SetActive(true);
    }

    public void AttackDisable()
    {
        attackCollider.SetActive(false);
    }
}
