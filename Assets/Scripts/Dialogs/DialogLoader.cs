using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public static class DialogLoader
{
    private static Dictionary<string, Dialog> sceneDialog;
    private static DialogController dc;

    private static bool initialized;
    private static string currentScene;

    private readonly static string PREF_PREFIX = "ACTIVE_DIALOG_";
    private static Action action;
    private static Sprite sp;


    public static void InitializeClass(DialogController dc, Button button, Action action)
    {
        if (!initialized)
        {
            initialized = true;
            LoadDialogData();
        }

        DialogLoader.dc = dc;
        DialogLoader.action = action;

        currentScene = SceneManager.GetActiveScene().name;

        if (sceneDialog.ContainsKey(currentScene))
        {
            SetSprite();
            SetButtonListener(button);
            DisplayIfFirst();
        }
    }

    private static void SetButtonListener(Button button)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => DisplayDialog());
    }

    private static void DisplayIfFirst()
    {
        if (PlayerPrefs.GetInt(PREF_PREFIX + currentScene, 0) == 0)
        {
            DisplayDialog();
        }
        PlayerPrefs.SetInt(PREF_PREFIX + currentScene, 1);
    } 

    private static void SetSprite()
    {
         DialogLoader.sp = Resources.Load<Sprite>($"Sprites/{GetDialog(currentScene).character_image}");
    }

    private static void LoadDialogData() {

        TextAsset jsonFile = Resources.Load<TextAsset>("Data/Dialogs");

        Dialogs dialogJson = JsonUtility.FromJson<Dialogs>(jsonFile.text);

        sceneDialog = new Dictionary<string, Dialog>();

        foreach (Dialog dialog in dialogJson.dialogs)
        {
            sceneDialog.Add(dialog.scene_name, dialog);
        }
    }

    public static void DisplayDialog()
    {
        Dialog dialog = GetDialog(currentScene);
        dc.ShowPanel(dialog.character_name, dialog.dialogs, sp, action);
    }

    private static Dialog GetDialog(string scene)
    {
        return sceneDialog.GetValueOrDefault(scene);
    }
    



}
