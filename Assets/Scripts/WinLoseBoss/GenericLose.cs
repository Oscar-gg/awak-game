using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; //cambio de escenas


public class GenericLose : MonoBehaviour
{

    private SceneInfoContainer sceneInfo;
    private Text message;
    private Button exitButton, retryButton;
    public GameObject anaSprite;

    private void Awake()
    {
        LoadData();
        message = GameObject.Find("ResultText").GetComponent<Text>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        setLoseAnimation();
        SetData();
    }
    void setLoseAnimation()
    {
        anaSprite.GetComponent<Animator>().SetTrigger("AnaLose"); //animacion del spriite de nina para perder]
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/LoseScenes");

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
                exitButton.onClick.AddListener(() => {
                    SceneManager.LoadScene(info.next_scene);
                });
                retryButton.onClick.AddListener(() => {
                    SceneManager.LoadScene(info.previous_scene);
                });
                return true;
            }
        }

        return false;
    }
}
