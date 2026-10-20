using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHurtPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(1f);
        }
    }
}
