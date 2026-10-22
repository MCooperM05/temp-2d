using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthBarManager : MonoBehaviour
{

    [SerializeField] public Slider healthBar;
    [SerializeField] public static float health = 20f;

    void Update()
    {
        //GameObject boss = GameObject.FindGameObjectWithTag("Boss");
      //  if (boss == null) {
     //       healthBar.value = health;
      //  }
    }
}
