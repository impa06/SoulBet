using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int money = 100;
    public int spinCost = 10;
    public int reward = 50;
    public int lives = 3;

    // Estos solo se usan en SceneMain
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public TMP_Text moneyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateHUD();
    }

    public void SpendMoney()
    {
        money -= spinCost;

        if (money <= 0)
        {
            LoseLife();
            money = 50;
        }

        UpdateHUD();
    }

    public void AddMoney()
    {
        money += reward;
        UpdateHUD();
    }

    public void LoseLife()
    {
        lives--;

        UpdateHearts();

        if (lives <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }

    public void UpdateHUD()
    {
        if (moneyText != null)
            moneyText.text = "Dinero: " + money;

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        if (heart1 != null) heart1.enabled = lives >= 1;
        if (heart2 != null) heart2.enabled = lives >= 2;
        if (heart3 != null) heart3.enabled = lives >= 3;
    }
}
