using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; //cambio de escenas


public class LoseScript : MonoBehaviour
{
    public Text resultText;
    public GameObject anaSprite;
    // Start is called before the first frame update
    void Start()
    {
        setLoseAnimation();
        resultText.text = "Suerte en tu próximo intento";
    }
    void setLoseAnimation()
    {
        anaSprite.GetComponent<Animator>().SetTrigger("AnaLose"); //animacion del spriite de nina para perder]
    }


    public void StartToPlay()
    {
        SceneManager.LoadScene("BossScene"); //llamar a la escena de juego
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); //ES PARA PDOER EXPORTAR EL JUEGO SINO VA A MARCAR ERROR
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
