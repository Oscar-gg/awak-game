using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; //cambio de escenas


public class WinScript : MonoBehaviour
{
    public Text resultText;
    public GameObject anaSprite;
    // Start is called before the first frame update
    void Start()
    {
        setWinAnimation();
        resultText.text = "Congratulations! c;";
    }
    void setWinAnimation()
    {
        anaSprite.GetComponent<Animator>().SetTrigger("AnaWin"); //animacion del spriite de nina para ganar
    }


    public void StartToPlay()
    {
        SceneManager.LoadScene("BossScene"); //llamar a la escena de juego
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(); //ES PARA PDOER EXPORTAR EL JUEGO SINO VA A MARCAR ERROR
    }

    // Update is called once per frame
    void Update()
    {

    }
}