using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Snelheid van de speler
    public float jumpForce = 10f; // Kracht van de sprong
    public float flipThreshold = 0.1f; // Drempelwaarde voor het omdraaien van richting
    private bool isGrounded = false; // Controleert of de speler op de grond is
    private Animator animator; // Referentie naar de Animator-component
    private Rigidbody2D rb; // Referentie naar de Rigidbody2D-component
    private bool movingRight = true; // Huidige bewegingsrichting van de speler

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Haal de Rigidbody2D-component op
        animator = GetComponent<Animator>(); // Haal de Animator-component op
    }

    void Update()
    {
        // Horizontale beweging
        float moveInput = Input.GetAxis("Horizontal"); // Krijg de horizontale invoer (A/D, Left/Right)
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Stel de snelheid van de speler in

        // Richting omdraaien als de snelheid significant is
        Flip(moveInput);

        // Animatie-instellingen
        animator.SetFloat("Horizontaal", Math.Abs(rb.velocity.x)); // Stel de horizontale animatieparameter in
        animator.SetFloat("Verticaal", rb.velocity.y); // Stel de verticale animatieparameter in

        // Springen
        if (Input.GetKeyDown(KeyCode.W) && isGrounded) // Controleer of de spatiebalk is ingedrukt en of de speler op de grond staat
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Geef de speler een opwaartse snelheid voor de sprong
            animator.SetBool("IsJumping", true); // Stel de Jumping-animatieparameter in
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Controleer of de speler de grond raakt
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // De speler raakt de grond
            animator.SetBool("IsJumping", false); // Stel de Jumping-animatieparameter in
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Controleer of de speler de grond verlaat
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // De speler verlaat de grond
        }
    }

    void Flip(float moveInput)
    {
        // Richting omdraaien als de snelheid significant is
        if (Math.Abs(moveInput) > flipThreshold) // Controleer of de horizontale invoer groter is dan de drempelwaarde
        {
            if (moveInput > 0 && !movingRight || moveInput < 0 && movingRight) // Controleer of de speler van richting moet veranderen
            {
                movingRight = !movingRight; // Richting omkeren
                Vector3 scale = transform.localScale; // Huidige schaal van de speler
                scale.x *= -1; // Richting omkeren door de x-schaal te vermenigvuldigen met -1
                transform.localScale = scale; // De schaal van de speler bijwerken
            }
        }
    }
}
