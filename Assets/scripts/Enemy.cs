using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    public float speed = 2f;
    public float detectionDistance = 1f;
    public LayerMask groundLayer; // Laagmasker voor de grond
    public LayerMask wallLayer; // Laagmasker voor de muur

    private bool movingRight = true;
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Beweeg de vijand naar links of rechts
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Voer raycasts uit om te controleren of de vijand de grond nadert
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(col.bounds.min, Vector2.down, detectionDistance, groundLayer);
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(col.bounds.max, Vector2.down, detectionDistance, groundLayer);

        // Als de vijand de rand nadert en niet meer op de grond staat, draai dan om
        if ((!leftRaycastHit.collider || !rightRaycastHit.collider) && !IsGrounded())
        {
            Flip();
        }
        else if (IsWallAhead())
        {
            Flip();
        }
    }

    bool IsGrounded()
    {
        // Controleer of de vijand op de grond staat
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + extraHeight, groundLayer);
        return hit.collider != null;
    }

    bool IsWallAhead()
    {
        // Controleer of er een muur voor de vijand is
        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center, movingRight ? Vector2.right : Vector2.left, detectionDistance, wallLayer);
        return hit.collider != null;
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}