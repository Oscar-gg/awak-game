using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DialogContentIntro : MonoBehaviour
{

    public string sceneName;

    public Sprite sp;


    // Start is called before the first frame update
    public void Start()
    {
        DialogLoader.InitializeClass(FindObjectOfType<DialogController>(), GameObject.Find("ButtonExplanation").GetComponent<Button>(), changeScene);
        DialogLoader.DisplayDialog();
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
