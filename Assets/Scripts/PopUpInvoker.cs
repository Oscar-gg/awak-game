using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInvoker : MonoBehaviour
{

    PopupGeneric genericPopup;

    public string scene;
    public string popupMessage;

    // Start is called before the first frame update

    private void Awake()
    {
        genericPopup = FindObjectOfType<PopupGeneric>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            genericPopup.ShowPopup(popupMessage, scene);
        }
    }
}
