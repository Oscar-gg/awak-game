using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour
{

    private TMP_Text userEmail;
    private TMP_Text userPassword;

    private Text statusText;

    // Start is called before the first frame update
    void Awake()
    {
        userEmail = GameObject.Find("UserEmail").GetComponent<TMP_Text>();
        userPassword = GameObject.Find("UserPassword").GetComponent<TMP_Text>();
        statusText = GameObject.Find("StatusText").GetComponent<Text>();
        PlayerPrefs.DeleteKey(Preferences.LOGIN_ERROR_PREF);
    }

    private void Update()
    {
        if (statusText.text != PlayerPrefs.GetString(Preferences.LOGIN_ERROR_PREF))
        {
            statusText.text = PlayerPrefs.GetString(Preferences.LOGIN_ERROR_PREF);
        }
    }

    public void LoginPressed()
    {
        StartCoroutine(LoginRoutine());
    }

    public IEnumerator LoginRoutine()
    {
        PlayerPrefs.SetString(Preferences.USER_EMAIL_PREF, userEmail.text);
        PlayerPrefs.SetString(Preferences.USER_PASSWORD_PREF, userPassword.text);
        yield return PlayerProgress.Instance.NewLogin();

        if (PlayerProgress.Instance.IsValidUser())
        {

            if (PlayerProgress.Instance.GetLevelsCompleted() > 0)
            {
                SceneManager.LoadScene(SceneNames.MAP);
            } else
            {
                SceneManager.LoadScene(SceneNames.INTRO);
            }
        }
    }
}
