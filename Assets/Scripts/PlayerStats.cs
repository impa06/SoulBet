using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Header("Vidas")]
    public int maxLives = 3;
    public int lives = 3;

    [Header("Economía")]
    public int money = 100;
    public int baseMoney = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;

        if (money <= 0)
        {
            money = 0;
            LoseLife();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    void LoseLife()
    {
        lives--;

        // ⭐⭐ ACTUALIZAR HUD ⭐⭐
        if (HeartsHUD.instance != null)
            HeartsHUD.instance.UpdateHearts();

        if (lives > 0)
        {
            // Resetear dinero si quedan vidas
            money = baseMoney;
        }
        else
        {
            money = 0;
            Debug.Log("GAME OVER — el jugador no tiene más vidas.");
            // Aquí puedes cargar escena de Game Over si quieres
        }
    }
}
