using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CofrePopUpMaze : MonoBehaviour
{
    public GameObject popUpPrefab;  // Prefab del panel del mensaje pop-up
    public Text popupMessageText;   // Referencia al componente Text en el panel pop-up

    void Start()
    {
        // Desactivar el panel pop-up al inicio del juego
        if (popUpPrefab != null)
        {
            popUpPrefab.SetActive(false);
        }

        Transform salirTransform = gameObject.transform.Find("salir");
        if (salirTransform != null)
        {
            Button salir = salirTransform.GetComponent<Button>();
            if (salir != null)
            {
                salir.onClick.AddListener(() => ClosePopup());
            }
            
        }
        

    }

    // Método llamado cuando ocurre una colisión
    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            string collidedTag = collision.otherCollider.tag; //Detectar el cofre
            string message = "";

            switch (collidedTag)
            {
                case "cofre1":
                    message = "cofre 1";
                    break;
                case "cofre2":
                    message = "cofre 2";
                    break;
                case "cofre3":
                    message = "cofre 3";
                    break;
                case "cofre4":
                    message = "cofre 4";
                    break;
                case "cofre5":
                    message = "cofre 5";
                    break;
                case "cofre6":
                    message = "cofre 6";
                    break;
                case "cofre7":
                    message = "cofre 7";
                    break;
                case "cofre8":
                    message = "cofre 8";
                    break;
                case "cofre9":
                    message = "cofre 9";
                    break;
                default:
                    message = "";
                    break;
            }

            Debug.Log("Mensaje para mostrar: " + message);

            
            ShowPopUp(message);
        }
    }

    void ShowPopUp(string message)
    {
        
        if (popUpPrefab != null && popupMessageText != null)
        {
            popupMessageText.text = message;
            popUpPrefab.SetActive(true);

        }
    }

    public void ClosePopup()
    {
        // Desactiva el panel pop-up
        if (popUpPrefab != null)
        {
            popUpPrefab.SetActive(false);
        }
    }





}
