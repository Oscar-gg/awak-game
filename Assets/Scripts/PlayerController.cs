using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public bool isMoving;

    private Vector2 input;
    private Rigidbody2D rb;
    private LayerMask solidObjectsLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        solidObjectsLayer = LayerMask.GetMask("SolidObjects");
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        float distance = Vector3.Distance(transform.position, targetPos);

        while (distance > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPos);
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
