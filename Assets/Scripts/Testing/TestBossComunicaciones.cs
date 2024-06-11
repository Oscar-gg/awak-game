using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBossComunicaciones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FinishRoutine());
    }

    private IEnumerator FinishRoutine()
    {
        yield return StartCoroutine(PlayerProgress.Instance.UpdateProgess(MiniGameNames.COMMUNICATIONS_BOSS, 100, 50));
        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneNames.WIN_G);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
