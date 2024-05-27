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
    private Coroutine damageCoroutine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(toggleRay());
        }
    }

    public IEnumerator toggleRay()
    {
        if (ligthOn)
        {
            ShowElement();
        } else
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

}
