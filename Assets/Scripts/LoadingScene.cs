using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainCoroutine());
    }


    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(PlayerProgress.Instance.NewLogin());
        SceneManager.LoadScene(PlayerPrefs.GetString(Preferences.RETURN_SCENE_PREF, SceneNames.LOGIN));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
