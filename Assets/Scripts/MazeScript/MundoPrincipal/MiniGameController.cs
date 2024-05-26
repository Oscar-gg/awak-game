using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    private GameProgressController progressController; // Referencia al GameProgressController asignada en el Inspector
    public string nextSceneName;

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
        progressController.CompleteMinigame(); // Llama al método CompleteMinigame del GameProgressController
        SceneManager.LoadScene(nextSceneName);
    }
}
