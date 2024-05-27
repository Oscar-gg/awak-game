using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameCompletionHandler : MonoBehaviour
{
    private GameProgressController progressController;
    public string nextSceneName; // Nombre de la escena a cargar después de completar el minijuego

    private void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();
    }

    // Este método debe ser llamado cuando el jugador complete un minijuego
    public void CompleteMinigame()
    {
        progressController.CompleteMinigame();

        // Carga la escena especificada
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no está especificado en MinigameCompletionHandler.");
        }
    }
}
