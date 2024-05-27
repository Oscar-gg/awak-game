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

        string[] dialogs = { "Bienvenido a AWAQ Estaciones Biológicas. Espero te encuentres muy bien.",
                             "Aquí en AWAQ nos encargamos de la conservación de los ecosistemas.",
                             "Esto lo logramos por medio de proyectos de reforestación y de biomonitorización."};

        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, changeScene);
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
