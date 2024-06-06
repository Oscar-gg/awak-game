using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using static PlayerControllerAna;

public class GameController : MonoBehaviour
{
    static public GameController Instance;
    public UIController uiController;
    public BossController bossController;
    public PlayerControllerAna playerControllerAna;
    public FighterStats fighterStats;
    public FighterAction fighterAction;
    public TurnsController turnsController;

    public int bosslife;

    private string ActualScene;

    private string ActualID;

    public string nextLevelName; // Nombre de la próxima escena/nivel

    public GameProgressController gameProgressController;

    // Start is called before the first frame update
    

    private void Awake()
    {
        StopAllCoroutines();
        bosslife = 10;
        PlayerPrefs.SetInt("lives", 5);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        gameProgressController = FindObjectOfType<GameProgressController>(); // Encuentra y referencia el GameProgressController

    }

    public void ActivateWinScene()
    {
        //Agregado
        CompleteMinigame(); // Actualiza el progreso al ganar

        SceneManager.LoadScene("WinScene");  
    }

    public void ActivateLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public int GetCurrentLives()
    {
        return PlayerPrefs.GetInt("lives"); //contador de vidas
    }

    public void SpendLives() //actualizar las vidas
    {
        int newLives = GetCurrentLives() - 1;
        PlayerPrefs.SetInt("lives", newLives);
        checkGameOver();
        uiController.UpdateLives();
    }

    public void UpdateAnaAnimation(PlayerAnimation nameAnimation)
    {
        Debug.Log("AnaHurt3");
        playerControllerAna.UpdateOutPlayerController();
        playerControllerAna.UpdateAnimation(nameAnimation);
        Debug.Log("AnaHurt3");

    }

    public void HitBoss(int dmg)
    {
        bosslife -= dmg;
        CheckWin();
    }

    public void CheckWin()
    {
        if(bosslife <= 0)
        {
            ActivateEndWinScene();
        }
    }

    public void checkGameOver()
    {
        if (PlayerPrefs.GetInt("lives") == 0) //condicion para activar la escena cuando pierdes
        {
            ActivateEndLoseScene();
        }
    }

    public void ActivateEndLoseScene()
    {

        ActualScene = SceneManager.GetActiveScene().name;

        if (ActualScene == "BossSceneComunicacion")
        {
            SceneManager.LoadScene("LoseSceneComunicacion");
        }
        else if (ActualScene == "BossSceneEtica")
        {
            SceneManager.LoadScene("LoseSceneEtica");
        }
        else if (ActualScene == "BossSceneSeguridad")
        {
            SceneManager.LoadScene("LoseSceneSeguridad");
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }

        playerControllerAna.UpdateOutPlayerController();
        playerControllerAna.UpdateAnimation(PlayerAnimation.AnaLose);

    }

    public void ActivateEndWinScene()
    {

        CompleteMinigame(); // Actualiza el progreso

        ActualScene = SceneManager.GetActiveScene().name;

        if (ActualScene == "BossSceneComunicacion")
        {
            SceneManager.LoadScene("WinSceneComunicacion");
        }
        else if (ActualScene == "BossSceneEtica")
        {
            SceneManager.LoadScene("WinSceneEtica");
        }
        else if (ActualScene == "BossSceneSeguridad")
        {
            SceneManager.LoadScene("WinSceneSeguridad");
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }

        playerControllerAna.UpdateOutPlayerController();
        playerControllerAna.UpdateAnimation(PlayerAnimation.AnaWin);

    }

    public void CompleteMinigame()
    {
        if (gameProgressController != null)
        {
            gameProgressController.CompleteMinigame(); // Actualiza el progreso en el GameProgressController
        }
        else
        {
            Debug.LogWarning("GameProgressController no encontrado");
        }
    }

    public string ObtainIDHint()
    {
        ActualID = turnsController.GetAttackID();
        return ActualID;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
