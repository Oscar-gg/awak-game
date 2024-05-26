using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;


    [Header("Stats")]
    public float health;
    public float attack;
    public float defense;
    public float range;
    public float speed;
    public float experience;
    public String character;

    private float startHealth;

    [HideInInspector]
    public int nextActTurn;

    public bool dead = false;

    //calcular size de la healthbar
    private Transform healthTransform;

    private Vector2 healthScale;

    private float xNewHealthScala;

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthTransform.localScale;

        startHealth = health;
    }

    public void ReceiveDamage(float damage)
    {
        Debug.Log("Receive damage");
        health = health - damage;
        //animator.Play("Damage");

        //set damage text

        if (health <= 0)
        {
            if(character == "Hero")
            {
                dead = true;
                gameObject.tag = "Dead";
                Destroy(healthFill);
                Destroy(gameObject);
                GameController.Instance.ActivateEndLoseScene();
            } else
            {
                dead = true;
                gameObject.tag = "Dead";
                Destroy(healthFill);
                Destroy(gameObject);
                GameController.Instance.ActivateEndWinScene();
            }


        } else if(damage > 0)
        {
            xNewHealthScala = healthScale.x * (health/startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScala, healthScale.y);
        }

        Invoke("ContinueGame", 2); //llamarlo 2 segundos despues
    }

    public bool GetDead()
    {
        return dead;
    }

    void ContinueGame()
    {
        GameObject.Find("TurnsController").GetComponent<TurnsController>().NextTurn();
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f/speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

}
