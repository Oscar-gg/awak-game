using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DialogContent : MonoBehaviour
{

    public Sprite sp;

    private bool isActive;
    private DialogController dialogController;

    private static readonly string MENU_NAME = "menu";

    // Start is called before the first frame update
    void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
        if (PlayerPrefs.GetInt(MENU_NAME, 0) == 0)
        {
            DialogActivation();
        }

        PlayerPrefs.SetInt(MENU_NAME, 1);
    }

    void Deactivate()
    {
        isActive = false;
    }

    public void DialogActivation()
    {
        if (!isActive)
        {
            isActive=true;
            callDialog();
        }
    }

    private void callDialog()
    {

        //Explora como gustes, pero recuerda que es muy importante completar los niveles.

        string[] dialogs = { "Este es el menú. Aquí verás los niveles.", "Tendrás que completarlos para poder superar tu travesía en AWAQ.",
                             "Explora como gustes, pero recuerda que es muy importante completar los niveles.",
                             };
        dialogController.ShowPanel("Instructor Awaq", dialogs, sp, Deactivate);
    }

}
