using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [SerializeField] public TMP_Text goldText;
    public float goldAmount = 0;

    public void SetGoldAmount(float amount)
    {
        goldAmount = amount;
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = "+" + goldAmount.ToString();
    }

    public void FadeGold()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
