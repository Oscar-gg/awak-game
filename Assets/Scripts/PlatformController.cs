using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5;
    public float jumpForce = 5;
    public Rigidbody2D rig; // referencia la Rigidbody del jugador
    private int jumpsRemaining = 2; // Cantidad de saltos que puede realizar 
    public SpriteRenderer sr;

    bool lookingLeft = false;

    Animator animatorController;

    Vector3 initialPosition;
    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Start()
    {
        // Para modificar propiedades de animaciones
        animatorController = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Hacer un salto cuando se presiona la flecha de arriba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpsRemaining > 0)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Emular salto agregando vector de fuerza
                jumpsRemaining--; // Reducir cantidad de saltos disponibles
            }
        }

        
    }

    public void FixedUpdate()
    {
        // obtener la velocidad de las flechitas
        float xInput = Input.GetAxis("Horizontal");


        rig.velocity = new Vector2(xInput * moveSpeed, rig.velocity.y);

        // Modificar apariencia de sprite, en función del movimiento
        if (rig.velocity.x > 0)
        {
            lookingLeft = false;
        }
        else if (rig.velocity.x < 0)
        {
            lookingLeft = true;
        }

        if (lookingLeft)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (rig.velocity.y != 0)
        {
            UpdateAnimation(PlayerAnimation.jump);
        }
        else if (xInput != 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        }
        else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }

    }

    // Resetear saltos
    public void resetJump()
    {
        jumpsRemaining = 2;
    }

    // Definir animaciones
    public enum PlayerAnimation
    {
        idle, walk, jump, die, run, hurt
    }

    // Modificar los estados de la animación
    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        animatorController.SetFloat("jumpY", rig.velocity.y);

        switch (nameAnimation)
        {
            case PlayerAnimation.idle:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", false);
                break;
            case PlayerAnimation.walk:
                animatorController.SetBool("isWalking", true);
                animatorController.SetBool("isJumping", false);
                break;
            case PlayerAnimation.jump:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", true);
                break;
            case PlayerAnimation.hurt:
                animatorController.SetTrigger("isHurt");
                break;
        }
    }

    public void collidedRay()
    {
        transform.position = initialPosition;
    }

}
