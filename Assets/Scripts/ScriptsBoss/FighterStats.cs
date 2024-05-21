using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private float startHealth;

    [HideInInspector]
    public int nextActTurn;

    public bool dead = false;

    //calcular size de la healthbar
    private Transform healthTransform;

    private Vector2 healthScale;

    private float xNewHealthScala;

    private void Start()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthTransform.localScale;

        startHealth = health;
    }

    public void ReceiveDamage(float damage)
    {
        health = health - damage;
        //animator.Play("Damage");

        //set damage text

        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        } else
        {
            xNewHealthScala = healthScale.x * (health/startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScala, healthScale.y);
        }
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

}
