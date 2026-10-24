using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LayerManager : MonoBehaviour
{
    [SerializeField] GameObject geiser1;
    [SerializeField] GameObject geiser2;
    [SerializeField] GameObject geiser3;
    [SerializeField] GameObject geiser4;
    
    void Start()
    {
        // Start the logic loop once
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        // 1. Wait for Boss
        yield return new WaitForSeconds(5f);

        // 2. Loop forever, triggering attacks
        while (true) 
        {
            yield return new WaitForSeconds(7f); // Wait 7 seconds between attacks
            TriggerRandomGeyser();
        }
    }

    void TriggerRandomGeyser()
    {
        float rand = UnityEngine.Random.value;
        
        // Pick one based on the random value
        if (rand > 0.75f) {
            geiser1.GetComponent<GeiserMove>().GoUp();
        } else if (rand > 0.5f) {
            geiser2.GetComponent<GeiserMove>().GoUp();
        } else if (rand > 0.25f) {
            geiser3.GetComponent<GeiserMove>().GoUp();
        } else {
            geiser4.GetComponent<GeiserMove>().GoUp();
        }
    }
}
