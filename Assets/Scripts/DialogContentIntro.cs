using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogContentIntro : MonoBehaviour
{

    public string sceneName;

    public Sprite sp;


    // Start is called before the first frame update
    public void Start()
    {
        DialogController dialogController = FindObjectOfType<DialogController>();

        string[] dialogs = { "Bienvenido a AWAQ Estaciones Biol�gicas. Espero te encuentres muy bien.",
                             "Aqu� en AWAQ nos encargamos de la conservaci�n de los ecosistemas.",
                             "Esto lo logramos por medio de proyectos de reforestaci�n y de biomonitorizaci�n."};

        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, changeScene);
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
