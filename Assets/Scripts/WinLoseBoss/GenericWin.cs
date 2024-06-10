using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenericEnding : MonoBehaviour
{

    private SceneInfoContainer sceneInfo;
    private Text message;
    private Button button;
    public GameObject anaSprite;

    private void Awake()
    {
        LoadData();
        message = GameObject.Find("ResultText").GetComponent<Text>();
        button = GameObject.Find("ContinueButton").GetComponent<Button>();
    }

    // Start is called before the first frame update


    void Start()
    {
        SetData();
        SetWinAnimation();
    }
    void SetWinAnimation()
    {
        anaSprite.GetComponent<Animator>().SetTrigger("AnaWin"); //animacion del spriite de nina para ganar
    }

    private void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/WinScenes");

        SceneInfoContainer sceneInfo = JsonUtility.FromJson<SceneInfoContainer>(jsonFile.text);

        this.sceneInfo = sceneInfo;
    }

    private bool SetData()
    {
        foreach (SceneInfo info in sceneInfo.SceneInfo)
        {
            if (info.minigame == PlayerPrefs.GetString(Preferences.PREVIOUS_GAME))
            {
                message.text = info.message;
                button.onClick.AddListener(() => {
                    SceneManager.LoadScene(info.next_scene);
                });
                return true;
            }
        }

        return false;
    }
}
