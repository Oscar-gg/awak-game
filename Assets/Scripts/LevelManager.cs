using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public string sceneName;

    public Sprite sp;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Initialize dialog
    public void Start()
    {
        DialogController dialogController = FindObjectOfType<DialogController>();

        string[] dialogs = { "Bienvenido a AWAQ Estaciones Biol�gicas. Espero te encuentres muy bien.",
                             "Aqu� en AWAQ nos encargamos de la conservaci�n de los ecosistemas.",
                             "Esto lo logramos por medio de proyectos de reforestaci�n y de biomonitorizaci�n."};

        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, changeScene);
    }

}
