using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using System.Text;


public class PlayerProgress : MonoBehaviour
{

    private static PlayerProgress _instance;

    public static PlayerProgress Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("PlayerProgress");
                _instance = singletonObject.AddComponent<PlayerProgress>();
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // User data
    private string email;
    private int id;

    private List<Zone> zones;

    private const int TOTAL_GAMES = 8;

    private bool initialized;

    private IEnumerator InitializePlayerProgress()
    {
        if (!initialized)
        {
            if (PlayerPrefs.GetString(Preferences.USER_EMAIL_PREF, "") == "" ||
                PlayerPrefs.GetString(Preferences.USER_PASSWORD_PREF, "") == "")
            {
                SceneManager.LoadScene(SceneNames.LOGIN);
            }

            yield return StartCoroutine(FetchDatabase());
            initialized = true;
        }
    }

    public float GetProgress()
    {
        CheckIfInitialized();
        return GetLevelsCompleted() / (float)TOTAL_GAMES;
    }

    public IEnumerator NewLogin()
    {
        initialized = false;
        yield return StartCoroutine(InitializePlayerProgress());
    }

    public IEnumerator UpdateProgess(string miniGameName, int puntaje, int tiempo)
    {
        bool init = CheckIfInitialized();

        if (init)
        {
            int minigameId = GetMinigameId(miniGameName);

            Partida p = new Partida(id, minigameId, puntaje, tiempo);

            string url = Endpoints.PostPartida();

            UnityWebRequest web = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(p));

            web.uploadHandler = new UploadHandlerRaw(bodyRaw);
            web.downloadHandler = new DownloadHandlerBuffer();
            web.SetRequestHeader("Content-Type", "application/json");

            web.useHttpContinue = true;
            var cert = new ForceAcceptAll();
            web.certificateHandler = cert;
            cert?.Dispose();

            yield return web.SendWebRequest();


            if (web.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error en API: " + web.error);
                PlayerPrefs.SetString(Preferences.LOGIN_ERROR_PREF, "Error en API: " + web.error);
            }
            else
            {
                Debug.Log("partida: " + web.downloadHandler.text);
                Partida partida = JsonConvert.DeserializeObject<Partida>(web.downloadHandler.text);
                bool updated = false;
                for(int i = 0; i < zones.Count && !updated; i++)
                {
                    if (zones[i].UpdateMiniGame(minigameId, partida.puntaje, partida.tiempo))
                        updated = true;
                }

                if (!updated)
                {
                    throw new Exception($"The minigame {miniGameName} wasn't updated!");
                }
            }
        }
    }

    public bool CanAccessMiniGame(string miniGame)
    {
        bool init = CheckIfInitialized();
        if (!init)
        {
            return false;
        }

        int levelsCompleted = GetLevelsCompleted();

        for (int i = 0; i < zones.Count; i++)
        {
            MiniGame m = zones[i].GetMiniGame(miniGame);
            if (m != null)
            {
                return m.IdGame <= levelsCompleted;
            }
        }

        return false;
    }

    public bool IsMiniGameCompleted(string miniGame)
    {
        bool init = CheckIfInitialized();
        if (!init)
        {
            return false;
        }

        for (int i = 0; i < zones.Count; i++)
        {
            MiniGame m = zones[i].GetMiniGame(miniGame);
            if (m != null)
            {
                return m.Points > 0;
            }
        }

        throw new Exception("Minigame " + miniGame + " not found!");
    }

    public int GetLevelsCompleted()
    {
        bool init = CheckIfInitialized();
        if (!init)
            return -1;

        int totalCompleted = 0;

        for (int i = 0; i < zones.Count; i++)
        {
            totalCompleted += zones[i].GetCompletedMinigames();
        }

        return totalCompleted;
    }

    public bool IsValidUser()
    {
        return name != null && id != -1;
    }

    public int GetPoints()
    {
        bool init = CheckIfInitialized();
        if (!init)
        {
            return -1;
        }

        int totalPoints = 0;
        for (int i = 0; i < zones.Count; i++)
        {
            totalPoints += zones[i].GetPoints();
        }

        return totalPoints;
    }
    private int GetMinigameId(string miniGameName)
    {
        for (int i = 0; i < zones.Count; i++)
        {
            MiniGame m = zones[i].GetMiniGame(miniGameName);
            if (m != null)
                return m.IdGame;
        }

        throw new Exception("Minigame " + miniGameName + "not found!");
    }

    private IEnumerator FetchDatabase()
    {
        yield return StartCoroutine(GetBasicData());

        if (id == -1)
        {
            SceneManager.LoadScene(SceneNames.LOGIN);
        }

        yield return StartCoroutine(GetGameData());
    }

    private IEnumerator GetBasicData()
    {
        // Fetch basic data, throw error if not found.
        string url = Endpoints.GetUserId(PlayerPrefs.GetString(Preferences.USER_EMAIL_PREF),
                                         PlayerPrefs.GetString(Preferences.USER_PASSWORD_PREF));
        UnityWebRequest web = UnityWebRequest.Get(url);
        web.useHttpContinue = true;
        var cert = new ForceAcceptAll();
        web.certificateHandler = cert;
        cert?.Dispose();

        yield return web.SendWebRequest();

        // Comprobar si hubo alg�n error
        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error en API: " + web.error);
            PlayerPrefs.SetString(Preferences.LOGIN_ERROR_PREF, "Error en API: " + web.error);
        }
        else
        {
            if (web.downloadHandler.text == "-1")
            {
                PlayerPrefs.SetString(Preferences.LOGIN_ERROR_PREF, "Usuario no encontrado");
                id = -1;
                email = null;
            }
            else
            {
                id = Convert.ToInt32(web.downloadHandler.text);
                email = PlayerPrefs.GetString(Preferences.USER_EMAIL_PREF);
            }
        }
    }

    private IEnumerator GetGameData()
    {
        string url = Endpoints.GetZonas(id);
        UnityWebRequest web = UnityWebRequest.Get(url);
        web.useHttpContinue = true;
        var cert = new ForceAcceptAll();
        web.certificateHandler = cert;
        cert?.Dispose();

        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error en API: " + web.error);
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
            zones = JsonConvert.DeserializeObject<List<Zone>>(web.downloadHandler.text);
            for (int i = 0; i < zones.Count; i++)
            {
                string minigamesUrl = Endpoints.GetMiniGames(zones[i].id);
                web = UnityWebRequest.Get(minigamesUrl);
                web.useHttpContinue = true;
                cert = new ForceAcceptAll();
                web.certificateHandler = cert;
                cert?.Dispose();

                yield return web.SendWebRequest();

                List<MiniGame> minigames = JsonConvert.DeserializeObject<List<MiniGame>>(web.downloadHandler.text);

                // Actualizar aquellos minijuegos que tengan datos guardados.


                for (int j = 0; j < minigames.Count; j++)
                {
                    string partidaUrl = Endpoints.GetPartida(minigames[j].IdGame, id);
                    web = UnityWebRequest.Get(partidaUrl);
                    web.useHttpContinue = true;
                    cert = new ForceAcceptAll();
                    web.certificateHandler = cert;
                    cert?.Dispose();

                    yield return web.SendWebRequest();

                    Partida partida = JsonConvert.DeserializeObject<Partida>(web.downloadHandler.text);
                    if (partida != null)
                    {
                        minigames[j].Points = partida.puntaje;
                        minigames[j].Time = partida.tiempo;
                    }

                }

                zones[i].SetMiniGames(minigames);
            }
        }
    }

    private bool CheckIfInitialized()
    {
        if (!initialized)
        {
            PlayerPrefs.SetString(Preferences.RETURN_SCENE_PREF, SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneNames.LOADING);
            return false;
        }

        return true;
    }
}
