using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SlotMachineController : MonoBehaviour
{
    [Header("Sprites de la tragaperras")]
    public List<Sprite> symbols;

    [Header("Slots (Images del Canvas)")]
    public Image slot1Image;
    public Image slot2Image;
    public Image slot3Image;

    [Header("UI")]
    public TMP_Text resultText;
    public TMP_Text moneyText;   // <- TEXTO DEL DINERO EN ESTA ESCENA

    [Header("Economía")]
    public int costPerSpin = 10;
    public int winReward = 50;

    void Start()
    {
        UpdateMoneyUI(); // ← actualiza el dinero al entrar
    }

    public void SpinSlots()
    {
        PlayerStats stats = PlayerStats.instance;

        if (stats == null)
        {
            Debug.LogError("PlayerStats.instance es NULL. Asegúrate de que PlayerStats NO se destruye al cambiar de escena.");
            return;
        }

        // Verificar dinero antes de jugar
        if (stats.money < costPerSpin)
        {
            resultText.text = "No tienes dinero.";
            return;
        }

        // Cobrar jugada
        stats.money -= costPerSpin;

        // Random de slots
        int r1 = Random.Range(0, symbols.Count);
        int r2 = Random.Range(0, symbols.Count);
        int r3 = Random.Range(0, symbols.Count);

        slot1Image.sprite = symbols[r1];
        slot2Image.sprite = symbols[r2];
        slot3Image.sprite = symbols[r3];

        // Verificar victoria
        if (r1 == r2 && r2 == r3)
        {
            resultText.text = "¡GANASTE!";
            stats.money += winReward;
        }
        else
        {
            resultText.text = "Perdiste";
        }

        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Dinero: $" + PlayerStats.instance.money;
    }
}
