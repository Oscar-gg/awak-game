using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class FinalPopUpMaze : MonoBehaviour
{
    public GameObject FinalPopMaze;
    private MinigameController completionHandler;
    private int totalCofres = 1;
    private List<string> collidedCofres = new List<string>();
    public float delayBeforeShowingPopup = 1f; 

    //private MinigameCompletionHandler completionHandler; // Agregar a todas las funciones de los minijuegos

    void Start()
    {
        completionHandler = FindObjectOfType<MinigameController>();

        if (FinalPopMaze != null)
        {
            FinalPopMaze.SetActive(false);
        }

        Transform terminarTransform = FinalPopMaze.transform.Find("Terminar");
        if (terminarTransform != null)
        {
            Button terminarButton = terminarTransform.GetComponent<Button>();
            if (terminarButton != null)
            {
                terminarButton.onClick.AddListener(() => EndGame());
            }
        }
    }

    public void ShowFinalPopUp(string tag)
    {
        if (!collidedCofres.Contains(tag))
        {
            collidedCofres.Add(tag);
            if (collidedCofres.Count >= totalCofres)
            {
                GameObject tempObj = new GameObject("TempCoroutineRunner");
                tempObj.AddComponent<TempCoroutineRunner>().StartCoroutineWithDelay(FinalPopMaze, delayBeforeShowingPopup);
            }
        }
    }

    void EndGame()
    {
        if (completionHandler != null)
        {
            completionHandler.CompleteMinigame();
            SceneManager.LoadScene(10);
        }
        else
        {
            Debug.LogError("completionHandler no está asignado.");
        }
    }

}

public class TempCoroutineRunner : MonoBehaviour
{
    public void StartCoroutineWithDelay(GameObject popup, float delay)
    {
        StartCoroutine(ShowPopupAfterDelay(popup, delay));
    }

    private IEnumerator ShowPopupAfterDelay(GameObject popup, float delay)
    {
        yield return new WaitForSeconds(delay);
        popup.SetActive(true);
        Destroy(gameObject);
    }
}
