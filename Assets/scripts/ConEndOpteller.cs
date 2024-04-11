using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCoinManager : MonoBehaviour
{
    public static TotalCoinManager Instance; // Singleton instance
    public TextMeshProUGUI totalCoinText; // Referentie naar de TextMeshProUGUI die het totale aantal munten weergeeft

    private int totalCoins = 0; // Totaal aantal munten verzameld over beide niveaus

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

    public void AddCoins(int amount)
    {
        // Voeg het opgegeven aantal munten toe aan de totale score
        totalCoins += amount;
    }

    public int GetTotalCoins()
    {
        // Geef het totale aantal munten terug
        return totalCoins;
    }

    public void DisplayTotalCoins()
    {
        // Update de tekst in de UI met het totale aantal munten
        totalCoinText.text = "Total Coins: " + totalCoins.ToString();
    }
}
