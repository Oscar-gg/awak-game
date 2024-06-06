using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private float attackMultiplier;

    [SerializeField]
    private float defenseMultiplier = 1;

    private FighterStats attackerStats;
    private FighterStats targetStats;

    private float damage = 1.0f;
    
    public void Attack(GameObject victim)
    {
        
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        damage = attackMultiplier * attackerStats.attack; 

        damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
        //owner.GetComponent<Animator>().Play(animationName);
        targetStats.ReceiveDamage(damage);
        
    }

    public void NoAttack(GameObject victim)
    {

        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        damage = 0;

        //owner.GetComponent<Animator>().Play(animationName);
        targetStats.ReceiveDamage(damage);

    }

}

