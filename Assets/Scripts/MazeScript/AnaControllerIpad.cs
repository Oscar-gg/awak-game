using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnaControllerIpad : MonoBehaviour
{
    public float moveSpeed;
    public GameObject controls; // GameObject que agrupa todos los controles

    public bool isMoving;

    public Vector2 input;
    private Rigidbody2D rb;
    private LayerMask solidObjectsLayer;

    private Animator animator;


    bool moveLeft;
    bool moveRight;
    bool moveUp;
    bool moveDown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        solidObjectsLayer = LayerMask.GetMask("SolidObjects");
    }

    //Left Button - button 
    public void PointerDownLeft()
    {
        Debug.Log("Pressed pointer down");

        moveLeft = true;
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    // Right Button - button 
    public void PointerDownRight()
    {
        moveRight = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }

    //Up button - button 
    public void PointerDownUp()
    {
        moveUp = true;

    }
    public void PointerUpUp()
    {
        moveUp = false;
    }

    //Down button - button 
    public void PointerDownDown()
    {
        moveDown = true;

    }
    public void PointerUpDown()
    {
        moveDown = false;
    }


    private void Update()
    {
        if (!isMoving)
        {
            // Inicializar las variables de entrada a 0
            float horizontalInput = 0f;
            float verticalInput = 0f;

            // Actualizar las variables de entrada según los botones presionados
            if (moveLeft) horizontalInput = -1f;
            else if (moveRight) horizontalInput = 1f;

            if (moveUp) verticalInput = 1f;
            else if (moveDown) verticalInput = -1f;

            // Crear el vector de movimiento basado en la entrada
            input = new Vector2(horizontalInput, verticalInput);

            // Si hay entrada de movimiento
            if (input != Vector2.zero)
            {
                Debug.Log("Movimiento detectado: " + input);

                // Establecer las animaciones
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                // Calcular la posición objetivo
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);

                // Si la posición objetivo es transitable, mover al personaje
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Collider2D collider = Physics2D.OverlapBox(targetPos, new Vector2(0.9f, 0.9f), 0f, solidObjectsLayer);

        return collider == null; // true si no hay colisión
    }

    



}
