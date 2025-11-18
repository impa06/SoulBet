using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SlotMachineController : MonoBehaviour
{
    public List<Sprite> symbols; // <- AQUI VAN TUS IMÁGENES PNG
    public Image slot1Image;
    public Image slot2Image;
    public Image slot3Image;
    public TMP_Text  resultText;

    public void SpinSlots()
    {
   

        int r1 = Random.Range(0, symbols.Count);
        int r2 = Random.Range(0, symbols.Count);
        int r3 = Random.Range(0, symbols.Count);

        slot1Image.sprite = symbols[r1];
        slot2Image.sprite = symbols[r2];
        slot3Image.sprite = symbols[r3];

        if (r1 == r2 && r2 == r3)
            resultText.text = "GANASTE!";
        else
            resultText.text = "PERDISTE";
    }
}
