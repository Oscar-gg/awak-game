using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    private GameProgressController progressController; // Referencia al GameProgressController asignada en el Inspector
    public string nextSceneName;

    private bool minigameCompleted = false; // Variable para controlar si el minijuego ya se ha completado

    private void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();
        if (progressController == null)
        {
            Debug.LogError("No se encontró un objeto GameProgressController en la escena.");
        }
    }

    public void CompleteMinigame()
    {
        if (!minigameCompleted) // Verifica si el minijuego ya se ha completado
        {
            progressController.CompleteMinigame(); // Llama al método CompleteMinigame del GameProgressController
            minigameCompleted = true; // Marca el minijuego como completado para evitar llamadas duplicadas
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
