using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximale gezondheid van de speler
    private int currentHealth; // Huidige gezondheid van de speler
    public TextMeshProUGUI healthText; // Referentie naar de TextMeshProUGUI voor de gezondheid

    void Start()
    {
        currentHealth = maxHealth; // Stel de huidige gezondheid in op de maximale gezondheid bij het starten van het spel
        UpdateUI(); // Werk de UI bij om de gezondheid weer te geven
    }

    // Methode om de gezondheid van de speler te verminderen
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Verminder de huidige gezondheid met het opgegeven schadebedrag
        UpdateUI(); // Werk de UI bij om de nieuwe gezondheid weer te geven

        if (currentHealth == 0)
        {
            Die(); // Als de gezondheid van de speler nul of minder is, roep de Die methode aan
        }
    }

    // Methode om de UI bij te werken met de huidige gezondheid
    void UpdateUI()
    {
        healthText.text = "Health: " + currentHealth.ToString(); // Update de tekst in de UI met de huidige gezondheid
    }

    // Methode die wordt aangeroepen wanneer de speler sterft
    void Die()
    {
        // Voeg hier eventueel extra logica toe die moet worden uitgevoerd wanneer de speler sterft
        Destroy(gameObject);
        Debug.Log("Player died!");
    }

    // Methode die wordt aangeroepen wanneer de speler botst met een andere collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Controleer of de speler botst met een vijand
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Controleer of de speler boven op de vijand staat (en niet tegen hem botst)
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y > 0.5f)
            {
                // Vernietig de vijand (laat hem "dood" gaan)
                Destroy(collision.gameObject);
            }
            else
            {
                // Verminder de gezondheid van de speler met 1
                TakeDamage(1);
            }
        }
    }
}