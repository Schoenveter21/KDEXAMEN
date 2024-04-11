using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Laat de speler door de muur gaan door de collider van de muur te negeren
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Herstel de interactie tussen de collider van de speler en de collider van de muur wanneer de speler de muur verlaat
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), false);
        }
    }
}
