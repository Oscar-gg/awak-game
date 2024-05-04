using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CofrePopUp : MonoBehaviour
{
    public GameObject popupPrefab;
    public string mensajePopup;

    private GameObject currentPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Instanciar el prefab del popup
            currentPopup = Instantiate(popupPrefab, transform.position, Quaternion.identity);

            // Configurar el mensaje del popup
            Text popupText = currentPopup.GetComponentInChildren<Text>();
            if (popupText != null)
            {
                popupText.text = mensajePopup;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Destruir el popup al salir de la colisión
            if (currentPopup != null)
            {
                Destroy(currentPopup);
            }
        }
    }
}
