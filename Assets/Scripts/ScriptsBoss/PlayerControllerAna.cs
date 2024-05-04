using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerAna : MonoBehaviour
{
    //declaramos variables para modificar el movimiento de nina
    public SpriteRenderer sr;
    public bool OutPlayerController = false;

    Animator animatorController;

    // Start is called before the first frame update
    //En el start vamos a obtener el componente que se va a animar
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OutPlayerController)
        {
            UpdateAnimation(PlayerAnimation.AnaIdle);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateAnimation(PlayerAnimation.AnaFight);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                UpdateAnimation(PlayerAnimation.AnaHurt);
            }
        }

        if (OutPlayerController)
        {
            OutPlayerController = false;
        }
    }

    public void UpdateOutPlayerController()
    {
        OutPlayerController = true;
    }

    public enum PlayerAnimation
    {
        AnaIdle, AnaFight, AnaHurt, AnaLose, AnaWin
    }


    public void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        //se hacen casos y actualizan valores para las transiciones puestas en "animator" unity
        switch (nameAnimation)
        {
            case PlayerAnimation.AnaIdle:
                animatorController.SetBool("isFighting", false);
                animatorController.SetBool("isHurt", false);
                animatorController.SetBool("win", false);
                animatorController.SetBool("lose", false);
                break;
            case PlayerAnimation.AnaFight:
                animatorController.SetBool("isFighting", true);
                animatorController.SetBool("isHurt", false);
                break;
            case PlayerAnimation.AnaHurt:
                animatorController.SetBool("isFighting", false);
                animatorController.SetBool("isHurt", true);
                break;
            case PlayerAnimation.AnaWin:
                animatorController.SetBool("win", true);
                animatorController.SetBool("lose", false);
                break;
            case PlayerAnimation.AnaLose:
                animatorController.SetBool("win", false);
                animatorController.SetBool("lose", true);
                break;

        }
    }
}
