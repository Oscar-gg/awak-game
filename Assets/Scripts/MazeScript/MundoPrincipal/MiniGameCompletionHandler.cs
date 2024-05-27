using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameCompletionHandler : MonoBehaviour
{
    private GameProgressController progressController;
    public string nextSceneName; // Nombre de la escena a cargar despu�s de completar el minijuego

    private void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();
    }

    // Este m�todo debe ser llamado cuando el jugador complete un minijuego
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
            Debug.LogWarning("El nombre de la escena no est� especificado en MinigameCompletionHandler.");
        }
    }
}
