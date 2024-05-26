using UnityEngine;
using UnityEngine.UI;

public class GameProgressController : MonoBehaviour
{
    public bool[] worldsUnlocked;
    public int totalMinigames = 6; // Total de minijuegos
    private int initialProgress = 0;
    public float currentProgress = 0f;
    public Image progressBar; // Barra de progreso UI
    public Text progressText; // Texto que muestra el progreso

    private void Start()
    {
        PlayerPrefs.SetFloat("CurrentProgress", currentProgress);
        LoadProgress();
        UpdateProgressUI();
    }

    //private void OnApplicationQuit()
    //{
    //    SaveProgress();
    //}

    public void UnlockWorld(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldsUnlocked.Length)
        {
            worldsUnlocked[worldIndex] = true;
            UpdateWorldUI();
        }
    }

    public void CompleteMinigame()
    {
        currentProgress += 1f / totalMinigames;
        if (currentProgress > 1f) currentProgress = 1f;
        SaveProgress();
        UpdateProgressUI();
    }

    //private void SaveProgress()
    //{
    //    for (int i = 0; i < worldsUnlocked.Length; i++)
    //    {
    //        PlayerPrefs.SetInt("WorldUnlocked_" + i, worldsUnlocked[i] ? 1 : 0);
    //    }
    //    PlayerPrefs.SetFloat("CurrentProgress", currentProgress);
    //    PlayerPrefs.Save();
    //}

    private void LoadProgress()
    {
        for (int i = 0; i < worldsUnlocked.Length; i++)
        {
            worldsUnlocked[i] = PlayerPrefs.GetInt("WorldUnlocked_" + i, i == 0 ? 1 : 0) == 1;
        }
        currentProgress = PlayerPrefs.GetFloat("CurrentProgress", 0f);
    }

    private void SaveProgress()
    {
        // Guardar el progreso actual en PlayerPrefs
        PlayerPrefs.SetFloat("CurrentProgress", currentProgress);
        PlayerPrefs.Save();
    }

    //private void LoadProgress()
    //{
    //    // Cargar el progreso guardado de PlayerPrefs
    //    currentProgress = PlayerPrefs.GetFloat("CurrentProgress", initialProgress);

    //    // Reiniciar el progreso si es la primera vez que se ejecuta el juego
    //    if (currentProgress == 0f)
    //    {
    //        ResetProgress();
    //    }
    //}

    private void ResetProgress()
    {
        // Reiniciar el progreso a su estado inicial
        currentProgress = initialProgress;
        // También podrías reiniciar otros valores aquí si fuera necesario
    }

    private void UpdateProgressUI()
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = currentProgress;
        }
        if (progressText != null)
        {
            progressText.text = (currentProgress * 100f).ToString("F1") + "%";
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

    //public void CompleteWorld(int worldIndex)
    //{
    //    if (worldIndex < totalMinigames)
    //    {
    //        currentProgress += 1f / totalMinigames;
    //        if (currentProgress > 1f) currentProgress = 1f;
    //        SaveProgress();
    //        UpdateProgressUI();
    //    }
    //}


    public bool IsWorldUnlocked(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldsUnlocked.Length)
        {
            return worldsUnlocked[worldIndex];
        }
        return false;
    }

}
