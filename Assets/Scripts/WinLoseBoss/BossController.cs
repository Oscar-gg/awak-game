using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControllerAna;

public class BossController : MonoBehaviour
{
    public GameObject anaSprite;
    public GameObject bossSprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Debug.Log("AnaHurt1");
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaHurt);
            GameController.Instance.SpendLives();
            Debug.Log("AnaHurt2");

        }

    }
}

