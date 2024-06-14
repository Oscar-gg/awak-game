using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System;
using System.Threading.Tasks;
using i5.Toolkit.Core.Utilities;
using Unity.VisualScripting;

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
        StartCoroutine(LoginRoutine(userEmail.text, userPassword.text));
    }

    public IEnumerator LoginRoutine(string email, string password)
    {
        PlayerPrefs.SetString(Preferences.USER_EMAIL_PREF, email);
        PlayerPrefs.SetString(Preferences.USER_PASSWORD_PREF, password);
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

    public void SignIn()
    {
        SignInWithAzureAd();
    }

    private async Task SignInWithAzureAd()
    {
        ServiceManager.GetService<OpenIDConnectService>().LoginCompleted += OnLoginCompleted;
        await ServiceManager.GetService<OpenIDConnectService>().OpenLoginPageAsync();
    }

    private async void OnLoginCompleted(object sender, EventArgs e)
    {
        var a = await ServiceManager.GetService<OpenIDConnectService>().GetUserDataAsync();

        ServiceManager.RemoveService<OpenIDConnectService>();
        ServiceManager.Disable();

        StartCoroutine(LoginRoutine(a.Email, "OAUTH"));
    }
}
