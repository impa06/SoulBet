using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text moneyText;

    void Start()
    {
        UpdateMoney();
    }

    void Update()
    {
        UpdateMoney();
    }

    void UpdateMoney()
    {
        if (PlayerStats.instance != null && moneyText != null)
        {
            moneyText.text = "Dinero: $" + PlayerStats.instance.money;
        }
    }
}
