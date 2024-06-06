using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndControl : MonoBehaviour
{
    private StartCredits music;

    void Start()
    {
        music = FindObjectOfType<StartCredits>();
        PlayerPrefs.SetInt("CreditIndex", 0);
    }

    public void GoMap()
    {
        DestroyObject();
        SceneManager.LoadScene("MenuScene");
    }

    public void GoLogin()
    {
        DestroyObject();
        SceneManager.LoadScene("RegistrationScene");
    }

    public void DestroyObject()
    {
        music.StopMusic();
    }
}
