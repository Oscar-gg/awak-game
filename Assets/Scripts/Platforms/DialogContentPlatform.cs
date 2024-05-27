using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DialogContentPlatform : MonoBehaviour
{

    public Sprite sp;

    private bool isActive;
    private DialogController dialogController;

    private static readonly string SCENE_NAME = "platform";

    // Start is called before the first frame update
    void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
        if (PlayerPrefs.GetInt(SCENE_NAME, 0) == 0)
        {
            DialogActivation();
        }

        PlayerPrefs.SetInt(SCENE_NAME, 1);
    }

    void Deactivate()
    {
        isActive = false;
    }

    public void DialogActivation()
    {
        if (!isActive)
        {
            isActive = true;
            callDialog();
        }
    }

    private void callDialog()
    {

        //Explora como gustes, pero recuerda que es muy importante completar los niveles.

        string[] dialogs = { "Aquí vas a tener que demostrar tus habilidades pasando este juego de plataforma.",
                               "Recolecta los conceptos de Seguridad. Cada que lo hagas, se te explicará su importancia.",
                             "¡Trata de aprendértelos, que los ocuparás más adelante!",
                             };
        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, Deactivate);
    }

}
