using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupMessageText;

    private void Start()
    {
       
        // Desactivar el panel pop-up al inicio del juego
        popupPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostrar el pop-up con el mensaje "Mundo 2"
            ShowPopup("¿Deseas entrar al mundo?");
        }
    }

    public void ShowPopup(string message)
    {
        // Mostrar el panel pop-up con el mensaje especificado
        popupMessageText.text = message;
        popupPanel.SetActive(true);
    }



    public void OnEnterButtonClicked()
    {
        Debug.Log("Opción 'Entrar' seleccionada");

        // Aquí puedes agregar la lógica para cambiar de escena
        SceneManager.LoadScene("NombreDeTuEscena");

        // Cerrar el pop-up después de seleccionar "Entrar"
        ClosePopup();
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("Opción 'Salir' seleccionada");

        // Cerrar el pop-up al seleccionar "Salir"
        ClosePopup();
    }

    private void ClosePopup()
    {
        // Cerrar el panel pop-up
        popupPanel.SetActive(false);
    }
}
