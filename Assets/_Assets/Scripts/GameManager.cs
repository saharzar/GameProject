using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public TextMeshProUGUI moneyText; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "$" + money.ToString();
    }
}
