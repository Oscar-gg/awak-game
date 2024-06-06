using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupGeneric : MonoBehaviour
{
    public Text popupMessageText;
    public string travelScene;
    private CanvasGroup CanvasGroup;

    private void Awake()
    {
        // Desactivar el panel pop-up al inicio del juego
        CanvasGroup = GetComponent<CanvasGroup>();
        Button salir = GameObject.FindWithTag("SalirPopup").GetComponent<Button>();
        salir.onClick.AddListener(() => ClosePopup());
        
        Button entrar = GameObject.FindWithTag("EntrarPopup").GetComponent<Button>();

        entrar.onClick.AddListener(() => OnEnterButtonClicked());
        Hide();
    }

    public void ShowPopup(string message, string scene)
    {
        // Mostrar el panel pop-up con el mensaje especificado
        popupMessageText.text = message;
        travelScene = scene;
        Show();
    }   

    public void ShowPopup()
    {
        Show();
    }

    public void OnEnterButtonClicked()
    {
        // Aquí puedes agregar la lógica para cambiar de escena
        SceneManager.LoadScene(travelScene);
        ClosePopup();
    }

    private void ClosePopup()
    {
        Hide();
    }

    public void Hide()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }
}
