using UnityEngine;
using TMPro;  

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [Header("Player Stats")]
    public int money = 0; 
    
    public delegate void MoneyChanged();
    public static event MoneyChanged OnMoneyChanged;

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
        
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Geld toegevoegd: +" + amount + " euro. Totaal: " + money);
        OnMoneyChanged?.Invoke();
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
            Debug.Log("Geld gespendeerd: -" + amount + " euro. Totaal: " + money);
            OnMoneyChanged?.Invoke();
            return true;
        }
        else
        {
            Debug.Log("Niet genoeg geld!");
            return false;
        }
    }
}