using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Checkar si la normal esta apuntando para arriba, con un threshold de 0.2
            if (Mathf.Abs(0.99f + contact.normal.y) < 0.2)
            {
                
                collision.gameObject.GetComponent<PlatformController>().resetJump();
                break;
            }
        }
    }
}
