using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private const int toggleTime = 5;

    private bool ligthOn = true;

    private Coroutine coroutine;
    private Coroutine moveCoro;
    private Coroutine rotateCoro;

    public bool XMovement = false;
    public bool flipHorizontally = false;

    private Coroutine damageCoroutine;

    private const float MOVE_DISTANCE = 0.05f;
    private readonly float MOVEMENT_DELAY = 0.05f;
    private readonly float MOVEMENT_BASE_SPEED = 0.05f;

    private float UP_TARGET;
    private float DOWN_TARGET;

    void Start()
    {
        StartCoroutine(MoveRay());
        StartCoroutine(Flip());
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(ToggleRay());
        }
    }

    public IEnumerator ToggleRay()
    {
        if (ligthOn)
        {
            ShowElement();
        }
        else
        {
            HideElement();
        }

        yield return new WaitForSeconds(toggleTime);
        ligthOn = !ligthOn;
        coroutine = null;
    }


    private void HideElement()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void ShowElement()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageRoutine(collision));
                FindObjectOfType<BossAudioManager>().PlaySound("damage");
            }

        }
    }

    public IEnumerator DamageRoutine(Collision2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetTrigger("isHurt");
        yield return new WaitForSeconds(1f);
        collision.gameObject.GetComponent<PlatformController>().collidedRay();
        damageCoroutine = null;
    }

    public IEnumerator MoveRay()
    {
        if (XMovement)
        {
            UP_TARGET = transform.position.x + MOVE_DISTANCE;
            DOWN_TARGET = transform.position.x - MOVE_DISTANCE;
        } else
        {
            UP_TARGET = transform.position.y + MOVE_DISTANCE;
            DOWN_TARGET = transform.position.y - MOVE_DISTANCE;
        }
        

        while (true)
        {
            moveCoro = StartCoroutine(MoveUp(UP_TARGET));
            while (moveCoro != null)
            {
                yield return new WaitForSeconds(0.1f);
            }
            moveCoro = StartCoroutine(MoveDown(DOWN_TARGET));
            while (moveCoro != null)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public IEnumerator Flip()
    {
        float SPEED = 0.05f;
        float AMOUNT = 180f;

        // Flip vertically
        Vector3 rotDir = new Vector3(1, 0, 0);

        if (flipHorizontally)
        {
            rotDir = new Vector3(0, 1, 0);
        }

        while (true)
        {
            rotateCoro = StartCoroutine(DoRotation(SPEED, AMOUNT, rotDir));
            while (rotateCoro != null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(2f);
            rotateCoro = StartCoroutine(DoRotation(SPEED, AMOUNT, rotDir * -1));

            while (rotateCoro != null)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator DoRotation(float speed, float amount, Vector3 axis)
    {
        Quaternion initialRotation = transform.rotation;
        float rot = 0f;
        while (rot < amount)
        {
            yield return null;
            float delta = Mathf.Max((amount - rot) * speed, 0.025f);
            transform.RotateAround(transform.position, axis, delta);
            rot += delta;
        }

        initialRotation.eulerAngles.Set(initialRotation.eulerAngles.x,
                                       initialRotation.eulerAngles.y + amount,
                                       initialRotation.eulerAngles.z);
        rotateCoro = null;
    }

    public IEnumerator MoveDown(float targetPose)
    {
        if (XMovement)
        {
            while (transform.position.x > targetPose)
            {

                Vector3 newPos = new Vector3(transform.position.x - MOVEMENT_BASE_SPEED, transform.position.y, transform.position.z);
                transform.position = newPos;

                yield return new WaitForSeconds(MOVEMENT_DELAY);
            }
        }
        else
        {
            while (transform.position.y > targetPose)
            {

                Vector3 newPos = new Vector3(transform.position.x, transform.position.y - MOVEMENT_BASE_SPEED, transform.position.z);
                transform.position = newPos;

                yield return new WaitForSeconds(MOVEMENT_DELAY);
            }
        }

        moveCoro = null;

    }

    public IEnumerator MoveUp(float targetPose)
    {
        if (XMovement)
        {
            while (transform.position.x < targetPose)
            {
                Vector3 newPos = new Vector3(transform.position.x + MOVEMENT_BASE_SPEED, transform.position.y, transform.position.z);
                transform.position = newPos;

                yield return new WaitForSeconds(MOVEMENT_DELAY);
            }
        } 
        else
        {
            while (transform.position.y < targetPose)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y + MOVEMENT_BASE_SPEED, transform.position.z);
                transform.position = newPos;

                yield return new WaitForSeconds(MOVEMENT_DELAY);
            }
        }
        
        moveCoro = null;
    }

}
