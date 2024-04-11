using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : MonoBehaviour
{
    public float speed = 8f; // Snelheid van de vijand
    public float detectionDistance = 1f; // Afstand waarop de vijand obstakels detecteert
    public LayerMask groundLayer; // Laagmasker voor de grond
    public LayerMask wallLayer; // Laagmasker voor de muur

    private bool movingRight = true; // Geeft aan of de vijand naar rechts beweegt
    private Collider2D col; // Referentie naar de collider van de vijand
    private Animator animator; // Animator component voor het afspelen van animaties

    void Start()
    {
        col = GetComponent<Collider2D>(); // Haal de Collider2D-component op van de vijand
        animator = GetComponent<Animator>(); // Haal de Animator-component op van de vijand
    }

    void Update()
    {
        // Beweeg de vijand naar links of rechts op basis van de bewegingsrichting
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); // Beweeg naar rechts
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // Beweeg naar links
        }

        // Voer raycasts uit om te controleren of de vijand de grond nadert
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(col.bounds.min, Vector2.down, detectionDistance, groundLayer); // Raycast naar links beneden
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(col.bounds.max, Vector2.down, detectionDistance, groundLayer); // Raycast naar rechts beneden

        // Als de vijand de rand nadert en niet meer op de grond staat, draai dan om
        if ((!leftRaycastHit.collider || !rightRaycastHit.collider) && !IsGrounded())
        {
            if (IsWallAhead()) // Als er een muur voor de vijand is
            {
                Flip(); // Draai de vijand om
            }
        }
        else if (IsWallAhead()) // Als er een muur voor de vijand is
        {
            Flip(); // Draai de vijand om
        }

        // Update de animator met de bewegingssnelheid van de vijand
        float moveSpeed = Mathf.Abs(speed) * (movingRight ? 1 : -1); // Neem de absolute waarde van de snelheid en pas de richting aan
        animator.SetFloat("fastenemymove", moveSpeed); // Stel de fastenemymove parameter in de animator in
    }

    bool IsGrounded()
    {
        // Controleer of de vijand op de grond staat door een raycast naar beneden te sturen
        float extraHeight = 0.1f; // Extra hoogte toegevoegd aan de raycast
        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + extraHeight, groundLayer); // Raycast naar beneden vanuit het midden van de collider
        return hit.collider != null; // Geef true terug als de collider wordt geraakt, anders false
    }

    bool IsWallAhead()
    {
        // Controleer of er een muur voor de vijand is door een raycast te sturen in de bewegingsrichting
        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, movingRight ? Vector2.right : Vector2.left, detectionDistance, wallLayer); // Raycast naar rechts of links op basis van de bewegingsrichting
        return hit.collider != null; // Geef true terug als de collider wordt geraakt, anders false
    }

    void Flip()
    {
        // Draai de richting van de vijand om door de schaal op de x-as om te keren
        movingRight = !movingRight; // Keer de bewegingsrichting om
        Vector3 scale = transform.localScale; // Huidige schaal van de vijand
        scale.x *= -1; // Keer de x-schaal om
        transform.localScale = scale; // Werk de schaal van de vijand bij
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Negeer de botsing als de vijand een andere vijand raakt
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Draai de vijand om als deze de speler raakt
            Flip();
        }
    }
}