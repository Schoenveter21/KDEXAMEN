using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Referentie naar de TextMeshProUGUI voor de gezondheid

    // Methode om de gezondheid van de speler weer te geven in de UI
    public void UpdateHealth(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth.ToString(); // Update de tekst in de UI met de huidige gezondheid
    }
}
