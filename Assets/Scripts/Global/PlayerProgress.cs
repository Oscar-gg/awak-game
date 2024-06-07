using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public static class PlayerProgress
{
    public static readonly string USER_EMAIL_PREF = "UserEmail";

    // User data
    private static string email;
    private static string role;
    private static int id;

    private static readonly string HOST_URL = "https://";

    private static readonly string LOGIN_SCENE = "RegistrationScene";

    private static List<Zone> zones;

    private static readonly int TOTAL_GAMES = 8;

    private static bool initialized;
    
    private static void InitializePlayerProgress()
    {
        if (!initialized)
        {
            if (PlayerPrefs.GetString(USER_EMAIL_PREF, "") == "")
            {
                SceneManager.LoadScene(LOGIN_SCENE);
            }
            
            FetchDatabase();
            initialized = true;
        }
    }

    public static float GetProgress()
    {
        InitializePlayerProgress();
        return GetLevelsCompleted() / (float) TOTAL_GAMES;
    }

    private static void SyncDatabase()
    {
        InitializePlayerProgress();
        
    }

    public static void UpdateProgess(string miniGameName, int puntaje, int tiempo)
    {
        InitializePlayerProgress();

        // Change progress in static class

        SyncDatabase();
    }

    private static void FetchDatabase()
    {

    }

    public static bool CanAccessMiniGame(string miniGame)
    {
        InitializePlayerProgress();

        int levelsCompleted = GetLevelsCompleted();

        for (int i = 0; i < zones.Count; i++)
        {
            MiniGame m = zones[i].GetMiniGame(miniGame);
            if (m != null)
            {
                return m.IdGame <= levelsCompleted;
            }
        }

        throw new System.Exception("Minigame not found!");
    }

    public static int GetLevelsCompleted()
    {
        InitializePlayerProgress();

        int totalCompleted = 0;

        for (int i = 0; i < zones.Count; i++)
        {
            totalCompleted += zones[i].GetCompletedMinigames();
        }

        return totalCompleted;
    }

    public static void InitializeBasicData()
    {
        // Fetch basic data, throw error if not found.
        
    }

    private static IEnumerator GetBasicData()
    {
        string JSONurl = $"https://192.168.1.119:5160/api/page/";
        Debug.Log(JSONurl);
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.useHttpContinue = true;
        var cert = new ForceAcceptAll();
        web.certificateHandler = cert;
        cert?.Dispose();

        yield return web.SendWebRequest();

        // Comprobar si hubo alg�n error
        if (web.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Error en API: " + web.error);
        }
        else
        {
            // Serializar el texto y hacerlo p�ginas.
            Debug.Log(web.downloadHandler.text);
            List<Page> pageList = JsonConvert.DeserializeObject<List<Page>>(web.downloadHandler.text);

            if (pageList.Count > 0)
            {
                //Sentences = Enumerable.Repeat<string>("", pageList.Count).ToArray<string>();
                //for (var i = 0; i < pageList.Count; i++)
                //{
                //    Sentences[i] = pageList[i].PageContent;
                //}
            }
        }
    }

}
