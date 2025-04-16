using UnityEngine;
using TMPro;  

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int money = 0;

    [Header("UI Elements")]
    public TMP_Text moneyText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Blijft bestaan tussen scenes
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateMoneyUI(); 
    }

    private void Start()
    {
        if (moneyText == null)
        {
            
        }
            
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI(); 
        Debug.Log("Geld toegevoegd: +" + amount + " euro. Totaal: " + money);
    }

    public bool HasEnoughMoney(int amount)
    {
        return money >= amount;
    }

    public bool SpendMoney(int amount)
    {
        if (HasEnoughMoney(amount))
        {
            money -= amount;
            UpdateMoneyUI(); 
            Debug.Log("Geld gespendeerd: -" + amount + " euro. Totaal: " + money);
            return true;
        }
        else
        {
            Debug.Log("Niet genoeg geld!");
            return false;
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "€" + money.ToString();  
        }
    }
}