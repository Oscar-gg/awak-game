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

        string[] dialogs = { "Bienvenido a AWAQ Estaciones Biológicas. Espero te encuentres muy bien.",
                             "Aquí en AWAQ nos encargamos de la conservación de los ecosistemas.",
                             "Esto lo logramos por medio de proyectos de reforestación y de biomonitorización."};

        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, changeScene);
    }

}
