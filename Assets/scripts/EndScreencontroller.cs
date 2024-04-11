using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    public GameObject endScreenUI; // Referentie naar het eindscherm-UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activeer het eindscherm-UI
            endScreenUI.SetActive(true);

            // Toon het totale aantal munten in het eindscherm
            TotalCoinManager.Instance.DisplayTotalCoins();
        }
    }
}
