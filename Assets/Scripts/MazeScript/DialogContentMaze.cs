using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DialogContentMaze: MonoBehaviour
{

    public Sprite sp;

    private bool isActive;
    private DialogController dialogController;

    private static readonly string SCENE_NAME = "LabeScene";

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

        string[] dialogs = { "Tendrás que explorar esta mazmorra con valentía, para poder recolectar los conceptos de Ética, los cuales ayudan a mantener un buen ambiente en AWAQ.",
                               "Cada que recolectes uno, se te explicará su importancia.",
                             "¡Trata de aprendértela, que la ocuparás más adelante!",
                             };
        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, Deactivate);
    }

}
