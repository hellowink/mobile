using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    // Velocidad del jugador
    public float speed = 5f;

    // Fuerza del salto
    public float jumpForce = 10f;

    // Booleano para verificar si el jugador está en el suelo
    private bool isGrounded = true;

    // Referencia al componente Rigidbody2D
    private Rigidbody2D rb;

    void Start()
    {
        // Obtener la referencia al componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtener la entrada horizontal del jugador
        float horizontalInput = Input.GetAxis("Horizontal");

        // Crear un vector de movimiento
        Vector2 movement = new Vector2(horizontalInput, 0);

        // Aplicar movimiento al jugador
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Verificar si el jugador quiere saltar y está en el suelo
        if (Input.GetKeyDown("space") && isGrounded)
        {
            // Aplicar fuerza hacia arriba para saltar
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Establecer isGrounded en falso
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador ha colisionado con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Establecer isGrounded en verdadero
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verificar si el jugador ha salido del suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Establecer isGrounded en falso
            isGrounded = false;
        }
    }
}

