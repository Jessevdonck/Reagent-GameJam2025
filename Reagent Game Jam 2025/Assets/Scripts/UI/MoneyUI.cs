using System;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    void Start()
    {
        UpdateMoneyUI(); 
    }

    private void OnEnable()
    {
        GameManager.OnMoneyChanged += UpdateMoneyUI;
    }
    
    void OnDisable()
    {
        GameManager.OnMoneyChanged -= UpdateMoneyUI;
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "€" + GameManager.Instance.money;
    }
}