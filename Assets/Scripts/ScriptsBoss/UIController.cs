using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Sprite spendLives;
    public Image[] livesImage;
    int lives = 5;

    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerPrefs.GetInt("lives", 5);
    }

    public void UpdateLives()
    {
        lives = GameController.Instance.GetCurrentLives(); //obtener la cantidad de vida
        if (lives > 0)
        {
            livesImage[lives].sprite = spendLives; //actializar los sprites de corazonez
        }
        GameController.Instance.checkGameOver(); //revisa la cantidad de vidas para el gameover
    }


    public void ReturnToMenu()
    {
        GameController.Instance.GoToMenu(); //regresar al menu
    }



    // Update is called once per frame
    void Update()
    {
        
    }


}
