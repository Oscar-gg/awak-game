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
    }

    private void ShowElement()
    {
        gameObject.GetComponent<Renderer>().enabled = true;

    }

}
