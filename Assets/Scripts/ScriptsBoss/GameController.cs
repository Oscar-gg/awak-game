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

    public int bosslife;

    private void Awake()
    {
        StopAllCoroutines();
        bosslife = 10;
        PlayerPrefs.SetInt("lives", 5);
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ActivateWinScene()
    {
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
        SceneManager.LoadScene("LoseScene");
        playerControllerAna.UpdateOutPlayerController();
        playerControllerAna.UpdateAnimation(PlayerAnimation.AnaLose);

    }

    public void ActivateEndWinScene()
    {
        SceneManager.LoadScene("WinScene");
        playerControllerAna.UpdateOutPlayerController();
        playerControllerAna.UpdateAnimation(PlayerAnimation.AnaWin);

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
