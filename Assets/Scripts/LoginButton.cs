using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour
{

    private TMP_Text userEmail;
    private TMP_Text userPassword;

    // Start is called before the first frame update
    void Awake()
    {
        userEmail = GameObject.Find("UserEmail").GetComponent<TMP_Text>();
        userPassword = GameObject.Find("UserPassword").GetComponent<TMP_Text>();
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
            SceneManager.LoadScene(SceneNames.INTRO);
        }
    }
}
