using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
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
        Debug.Log("Sign in done!");
        var a = await ServiceManager.GetService<OpenIDConnectService>().GetUserDataAsync();

        Debug.Log("Obtained data:");
        Debug.Log("a.Email");
        Debug.Log(a.Email);
        Debug.Log("a.FullName");
        Debug.Log(a.FullName);
        Debug.Log("a.Username");
    }
}
