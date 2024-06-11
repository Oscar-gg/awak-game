using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;



public class FinalPopUpMaze : MonoBehaviour
{
    public GameObject FinalPopMaze;
    private MinigameController completionHandler;
    private PlayerProgress progress;
    private int totalCofres = 7; //Cantidad total de cofres que se deberan de recoger 
    private List<string> collidedCofres = new List<string>();
    public float delayBeforeShowingPopup = 1f;

    private int tiempojugado;
    private Coroutine timeCoro;

    private void Awake()
    {
        
        tiempojugado = 0;
    }


    void Start()
    {
        completionHandler = FindObjectOfType<MinigameController>();
        progress = FindObjectOfType<PlayerProgress>();
        timeCoro = StartCoroutine(ContarTiempo());

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
        StartCoroutine(FinishRoutine());

        //if (completionHandler != null)
        //{
        //    completionHandler.CompleteMinigame();
        //    SceneManager.LoadScene(10);
        //    //StartCoroutine(UpdateProgressCoroutine());
        //}
        //else
        //{
        //    Debug.LogError("completionHandler no está asignado.");
        //}
    }


    private IEnumerator UpdateProgressCoroutine()
    {
        if (progress != null)
        {
            // Asegúrate de pasar los parámetros correctos
            yield return StartCoroutine(progress.UpdateProgess("NombreDelMinijuego", 100, 120));
        }
        else
        {
            Debug.LogError("progress no está asignado.");
        }
    }

    private IEnumerator FinishRoutine()
    {
        StopCoroutine(timeCoro);
        int points = 1000 + 100 + 2000 / tiempojugado;
        yield return PlayerProgress.Instance.UpdateProgess(MiniGameNames.MAZE, points, tiempojugado);

        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneNames.WIN_G);
    }

    private IEnumerator ContarTiempo()
    {
        while (true)
        {
            tiempojugado++;
            yield return new WaitForSeconds(1);
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



