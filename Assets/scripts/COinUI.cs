using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager Instance; // Singleton instance

    public TextMeshProUGUI coinText; // Referentie naar de TextMeshProUGUI die het aantal munten weergeeft

    private int totalCoins = 0; // Totaal aantal munten

    private void Awake()
    {
        // Singleton-patroon implementatie
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Vernietig het duplicaat
        }
    }

    public void UpdateCoinCount(int coinValue)
    {
        // Voeg de waarde van de munt toe aan de totale score
        totalCoins += coinValue;

        // Werk de UI bij met het nieuwe aantal munten
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update de tekst in de UI met het nieuwe aantal munten
        coinText.text = "Coins: " + totalCoins.ToString();
    }
}
