using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinCollectingScript : MonoBehaviour
{
    public int coinValue = 1; // Waarde van de munt

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin(); // Roep de methode aan om de munt te verzamelen
        }
    }

    void CollectCoin()
    {

        // Vernietig de munt
        Destroy(gameObject);
        // Stuur een signaal naar een ander script om het aantal munten bij te werken
        CoinUIManager.Instance.UpdateCoinCount(coinValue);
    }
}
