using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CofrePopUpMaze : MonoBehaviour
{
    public GameObject popUpPrefab;  // Prefab del panel del mensaje pop-up
    public Text popupMessageText;   // Referencia al componente Text en el panel pop-up
    public FinalPopUpMaze finalPopUpMaze;


    //public int targetCollisions = 1; // N�mero de colisiones objetivo
    //private int collisionCount = 0;  // Contador de colisiones

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

    // M�todo llamado cuando ocurre una colisi�n
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {

            string collidedTag = collision.otherCollider.tag; //Detectar el cofre
            string message = "";

            switch (collidedTag)
            {
                case "cofre1":
                    message = "Establecer principios �ticos y pautas de conducta para guiar las acciones de la organizaci�n ";
                    break;
                case "cofre2":
                    message = "Principales principios �ticos\n1) Respeto\n2) Integridad\n3) Responsabilidad\n4) Profesionalidad\n5) Compromiso\n6) Transparencia\n7) Di�logo";
                    break;
                case "cofre3":
                    message = "3 Principales principios �ticos\n1) Respeto \r\n2) Di�logo\r\n3) Trabajo en equipo   \r\n";
                    break;
                case "cofre4":
                    message = "Medidas que se toman para asegurar que se cumpla el c�digo �tico \r\n \nSe establecen sistemas de seguimiento y control para garantizar el cumplimiento \r\n";
                    break;
                case "cofre5":
                    message = "�Qui�n se encarga de que el c�digo de �tica se cumpla?\r\n\nLa direcci�n es la responsable de encargarse que el c�digo de �tica se cumpla\r\n";
                    break;
                case "cofre6":
                    message = "El c�digo �tico tiene como objetivo establecer principios �ticos y pautas de conducta para guiar las acciones de la organizaci�n ";
                    break;
                case "cofre7":
                    message = "Se espera a que los trabajadores de AWAQ act�en con:\r\n\n1) Eficacia\r\n2) Honradez\r\n3) Profesionalidad\r\n4) Responsabilidad\r\n";
                    break;
                case "cofre8":
                    message = "No se deben de aceptar ni ofrecer REGALOS NI BENEFICIOS los cuales se puede comprometer la imparcialidad";
                    break;
                case "cofre9":
                    message = "cofre 9";
                    break;
                default:
                    message = "";
                    break;
            }
            //collisionCount++;
            ShowPopUp(message);
            // Llama a la funci�n en FinalPopUpMaze para manejar la recolecci�n del cofr
            finalPopUpMaze.ShowFinalPopUp(collidedTag); // Notifica al FinalPopUpMazee


            //if (collisionCount >= targetCollisions)
            //{
            //    ShowPopUp("�Felicidades!\n\n Poderes obtenidos en este m�dulo:\r\n1) Respeto \r\n2) Integridad \r\n3) Responsabilidad \r\n4) Profesionalidad \r\n5) Compromiso \r\n6) Transparencia \r\n7) Di�logo \r\n8) Trabajo en equipo \r\n");
            //    StartCoroutine(GoToMainMenuAfterDelay(10));
            //}

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

    //IEnumerator GoToMainMenuAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene(10);
    //}





}
