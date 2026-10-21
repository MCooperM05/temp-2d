using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private Rigidbody2D enemyRb;

    [Header ("Damage Settings")]
    [SerializeField] private float damage;
    [SerializeField] private GameObject attackCollider;
    
    private GameObject player;
    private Transform playerTransform;
    private bool isAttacking = false;
    // private Vector2 currentMovement;
    public float smoothTime = 0.3f;
    private Vector3 velocity = new Vector3(0,0,0);


    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) Debug.Log("Player found");
        }
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    public void HandleRotation()
    {
        playerTransform = player.transform;

        Vector3 targ = playerTransform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void HandleMovement()
    {
        transform.position = Vector3.SmoothDamp(
        transform.position,
        playerTransform.position,
        ref velocity,
        smoothTime * moveSpeed
        );
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
