using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupGeneric : MonoBehaviour
{
    public Text popupMessageText;
    private string travelScene;

    private void Start()
    {
        // Desactivar el panel pop-up al inicio del juego
        gameObject.SetActive(false);

        Button salir = gameObject.transform.Find("salir").GetComponent<Button>();
        salir.onClick.AddListener(() => ClosePopup());

        Button entrar = gameObject.transform.Find("entrar").GetComponent<Button>();
        entrar.onClick.AddListener(() => OnEnterButtonClicked());
    }

    public void ShowPopup(string message, string scene)
    {
        // Mostrar el panel pop-up con el mensaje especificado
        popupMessageText.text = message;
        travelScene = scene;
        gameObject.SetActive(true);
    }

    public void OnEnterButtonClicked()
    {
        // Aquí puedes agregar la lógica para cambiar de escena
        SceneManager.LoadScene(travelScene);
        ClosePopup();
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
