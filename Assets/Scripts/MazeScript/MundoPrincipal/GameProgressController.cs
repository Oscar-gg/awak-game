using UnityEngine;
using UnityEngine.UI;

public class GameProgressController : MonoBehaviour
{
    public bool[] worldsUnlocked;
    public int totalMinigames = 6; // Total de minijuegos
    public float initialProgress = 0f; // Progreso inicial
    public int minigamesPerWorld = 2; // Número de minijuegos por mundo

    public float currentProgress = 0f;
    public Image progressBar; // Barra de progreso UI
    public Text progressText; // Texto que muestra el progreso

    private void Start()
    {
        Debug.Log("GameProgressController started");
        LoadProgress();
        Debug.Log("Progress loaded: " + currentProgress);
        UpdateProgressUI();
        Debug.Log("UI updated: " + (currentProgress * 100f).ToString("F1") + "%");
    }

    public void UnlockWorld(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldsUnlocked.Length)
        {
            worldsUnlocked[worldIndex] = true;
            UpdateWorldUI();
        }
    }

    public void UnlockNextWorld()
    {
        // Calcula la cantidad de mundos que se deben desbloquear
        int worldsToUnlock = Mathf.FloorToInt(currentProgress / minigamesPerWorld);

        // Itera sobre los mundos que se deben desbloquear
        for (int i = 0; i < worldsToUnlock; i++)
        {
            // Verifica si el mundo aún no está desbloqueado y está dentro del rango
            if (i < worldsUnlocked.Length && !worldsUnlocked[i])
            {
                worldsUnlocked[i] = true;
                UpdateWorldUI(); // Actualiza la interfaz para mostrar que el mundo ha sido desbloqueado
            }
        }
    }

    public void CompleteMinigame()
    {
        Debug.Log("Minigame completed!");
        currentProgress += 1f;
        if (currentProgress > totalMinigames) currentProgress = totalMinigames;
        SaveProgress(currentProgress); // Pasamos el progreso actual como argumento
        UpdateProgressUI();
    }




    private void SaveProgress(float progress) 
    {
        Debug.Log("Saving progress: " + progress);
        PlayerPrefs.SetFloat("CurrentProgress", progress);
        PlayerPrefs.Save();
    }


    private void LoadProgress()
    {
        currentProgress = PlayerPrefs.GetFloat("CurrentProgress", initialProgress);
        Debug.Log("Loaded progress: " + currentProgress);

        for (int i = 0; i < worldsUnlocked.Length; i++)
        {
            worldsUnlocked[i] = PlayerPrefs.GetInt("WorldUnlocked_" + i, i == 0 ? 1 : 0) == 1;
        }
    }

    private void ResetProgress()
    {
        Debug.Log("Resetting progress");
        PlayerPrefs.DeleteKey("CurrentProgress");  // Borra el progreso anterior
        for (int i = 0; i < worldsUnlocked.Length; i++)
        {
            PlayerPrefs.DeleteKey("WorldUnlocked_" + i);  // Borra el estado de desbloqueo de los mundos
        }
        PlayerPrefs.Save();
    }

    private void UpdateProgressUI()
    {
        Debug.Log("Updating progress UI: " + currentProgress);
        if (progressBar != null)
        {
            progressBar.fillAmount = currentProgress / totalMinigames;
            Debug.Log("ProgressBar updated: " + progressBar.fillAmount);
        }
        if (progressText != null)
        {
            float percentage = (currentProgress / totalMinigames) * 100f;
            progressText.text = percentage.ToString("F1") + "%";
            Debug.Log("ProgressText updated: " + progressText.text);
        }
    }




    private void UpdateWorldUI()
    {
        WorldColliderController[] worldControllers = FindObjectsOfType<WorldColliderController>();
        foreach (var controller in worldControllers)
        {
            controller.UpdateWorldState();
        }
    }

    public bool IsWorldUnlocked(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldsUnlocked.Length)
        {
            return worldsUnlocked[worldIndex];
        }
        return false;
    }
}
