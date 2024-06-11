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

    public Image popupImage;   // Componente Image para mostrar la imagen del cofre
    



    // Variables para almacenar las im�genes de los cofres
    public Sprite imagenCofre1;
    public Sprite imagenCofre2;
    public Sprite imagenCofre3;
    public Sprite imagenCofre4;
    public Sprite imagenCofre5;
    public Sprite imagenCofre6;
    public Sprite imagenCofre7;
    public Sprite imagenCofre8;
    public Sprite imagenCofre9;



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

    // Metodo llamado cuando ocurre una colisi�n
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {

            string collidedTag = collision.otherCollider.tag; //Detectar el cofre
            string message = "";

            switch (collidedTag)
            {
                case "cofre1":
                    popupImage.sprite = imagenCofre1;
                    message = "Respeto:\nTratar a todas las personas dignamente, sin discriminaci�n por sexo, raza, nacionalidad o religi�n. Colaborar e impulsar pol�ticas inclusivas";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre2":
                    message = "Principales principios �ticos\n1) Respeto\n2) Integridad\n3) Responsabilidad\n4) Profesionalidad\n5) Compromiso\n6) Transparencia\n7) Di�logo";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre3":
                    popupImage.sprite = imagenCofre3;
                    message = "Integridad:\nActuar con imparcialidad, justicia y buena fe. Evitando cualquier beneficio personal de los recursos p�blicos.";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre4":
                    popupImage.sprite = imagenCofre4;
                    message = "Responsabilidad:\nRespetar la legislaci�n, actuar honestamente y ejemplarmente, teniendo en cuenta las consecuencias. Reconociendo errores, solucion�ndolos y evitando su repetici�n. ";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre5":
                    popupImage.sprite = imagenCofre5;
                    message = "Profesionalidad:\nDedicaci�n, entrega y compromiso con la calidad y mejora continua. Buscando la excelencia profesional mediante la mejora de conocimientos y capacidades. ";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre6":
                    popupImage.sprite = imagenCofre6;
                    message = "Compromiso:\nLealtad y compromiso con la organizaci�n, as� como los proyectos y programas en los que se est� comprometido. ";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre7":
                    popupImage.sprite = imagenCofre7;
                    message = "Transparencia:\nAcreditar de forma veraz y completa los resultados y criterios seguidos, respondiendo diligentemente a las demandas de informaci�n.";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre8":
                    popupImage.sprite = imagenCofre8;
                    message = "Di�logo:\nTrabajar en equipo, compartir conocimiento y fomentar la participaci�n y la comunicaci�n efectiva. Reconocer el derecho a opinar de los involucrados, excluyendo cualquier muestra de superioridad. ";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                case "cofre9":
                    popupImage.sprite = imagenCofre9;
                    message = "Trabajo en equipo:\nRespeto a la diferente cultura, di�logo y trabajo en equipo, para lograr el cumplimiento de las metas establecidas. ";
                    FindObjectOfType<BossAudioManager>().PlaySound("Chest");
                    break;
                default:
                    message = "";
                    break;
            }

            ShowPopUp(message);
            // Llama a la funci�n en FinalPopUpMaze para manejar la recolecci�n del cofr
            finalPopUpMaze.ShowFinalPopUp(collidedTag); // Notifica al FinalPopUpMazee


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
