using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; //cambio de escenas


public class WinScript : MonoBehaviour
{
    private string ActualScene;

    public Text WinText;

    public GameObject anaSprite;
    // Start is called before the first frame update
    void Start()
    {
        setWinAnimation();
        GetLevelText();

    }
    void setWinAnimation()
    {
        anaSprite.GetComponent<Animator>().SetTrigger("AnaWin"); //animacion del spriite de nina para ganar
    }


    public void StartToPlay()
    {
        SceneManager.LoadScene("MenuScene"); //llamar a la escena de juego
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); //ES PARA PDOER EXPORTAR EL JUEGO SINO VA A MARCAR ERROR
    }

    public void WinBoss()
    {
        ActualScene = SceneManager.GetActiveScene().name;

        if (ActualScene == "WinSceneComunicacion")
        {
            SceneManager.LoadScene("MundoComunicacion");
        }
        else if (ActualScene == "LoseSceneEtica")
        {
            SceneManager.LoadScene("MundoEtica");
        }
        else if (ActualScene == "WinSceneSeguridad")
        {
            SceneManager.LoadScene("MundoSeguridad");
        }
        else if (ActualScene == "WinSceneTEDI")
        {
            SceneManager.LoadScene(SceneNames.CREDITS);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void WinEtica()
    {
        SceneManager.LoadScene("MundoEtica");
    }

    public void WinSeguridad()
    {
        SceneManager.LoadScene("MundoSeguridad");
    }

    public void WinComunicacion()
    {
        SceneManager.LoadScene("MundoComunicacion");
    }
    public void ContinueBoss()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GetLevelText()
    {
        ActualScene = SceneManager.GetActiveScene().name;

        if (ActualScene == "WinSceneComunicacion")
        {
            WinText.text = "Parece ser que un nuevo poder ha despertado en ti, el poder de comunicarte efectivamente con todos los Rangers de AWAQ. Gracias a esta vas a poder llevar a cabo todos los proyectos que tengas. Pero tu aventura continua, ¡sigue adelante!\r\n";
        }
        else if (ActualScene == "WinSceneEtica")
        {
            WinText.text = "Tienes la suficiente capacidad, valentía y ética para poder ser una gran persona y Ranger durante toda tu travesía en AWAQ. Pero tu aventura no termina, ¡sigue creciendo para ayudar a las personas junto con AWAQ!\r\n!\r\n";
        }
        else if (ActualScene == "WinSceneSeguridad")
        {
            WinText.text = "Ahora eres capaz de proteger tu seguridad y evitar cualquiera amenaza durante tu travesía en AWAQ. Podrás hacer todas tus actividades de manera más segura. Pero esto no es todo, tu aventura continua, ¡sigue adelante!\r\n";
        }
        else if (ActualScene == "WinSceneTEDI")
        {
            WinText.text = "Ahora que dominas los poderes de las Tecnologías de la Información, puedes aplicar tus conocimientos como Technology Ranger para ayudar a los demás. Pero tu aventura no termina, ¡sigue creciendo para ayudar a las personas junto con AWAQ!\r\n\r\n";
        }
        else
        {
            WinText.text = "Felicitaciones, tu siguiente aventura aguarda!";
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}