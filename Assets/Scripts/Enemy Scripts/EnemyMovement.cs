using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) Debug.Log("Player found");
        }
    }

    void Update()
    {
        Transform TargetObjTransform = player.transform;

        Vector3 targ = TargetObjTransform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
