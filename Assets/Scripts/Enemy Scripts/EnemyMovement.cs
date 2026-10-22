using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private Rigidbody2D rb;

    [Header("Melee Settings")]
    [SerializeField] private bool isMelee = true; // if the enemy does melee damage
    [SerializeField] private GameObject meleeObj;
    [SerializeField] private float attackCooldown = 1f;
    private bool isAttacking = false;
    private bool canAttack = true; // helps with attack cooldown

    [Header("References")]
    [SerializeField] private Animator anim;
    
    private GameObject player;
    private Transform playerTransform;
    public float smoothTime = 0.3f;
    private Vector3 velocity = new Vector3(0,0,0);


    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) Debug.Log("Player found");
        }
        playerTransform = player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking) HandleRotation();
        if (isAttacking && isMelee && canAttack)
        {
            canAttack = false;
            HandleAttack();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking) HandleMovement();
    }

    public void HandleMovement() // Handle enemy movement
    {
        Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void HandleRotation()
    {
        Vector3 targ = playerTransform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    public void HandleAttack()
    {
        anim.SetBool("Attack", true);
        StartCoroutine(UseAttack());
    }

    IEnumerator UseAttack()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("Attack", false);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown() // canAttack set to true so the enemy can attack again
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void EnableAttack()
    {
        meleeObj.SetActive(true);
    }

    public void DisableAttack()
    {
        meleeObj.SetActive(false);
    }
}
