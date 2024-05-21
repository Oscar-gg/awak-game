using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;


    [SerializeField]
    private GameObject respetoPrefab;

    [SerializeField]
    private GameObject honestidadPrefab;

    [SerializeField]
    private GameObject seguridadPrefab;

    private GameObject currentAttack;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if (tag == "Hero")
        {
            victim = enemy;
        }
        if (btn.CompareTo("respeto") == 0)
        {
            respetoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueRespeto");
        } else if (btn.CompareTo("honestidad") == 0)
        {
            honestidadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueHonestidad");
        } else if (btn.CompareTo("seguridad") == 0)
        {
            seguridadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueSeguridad");
        } else
        {
            Debug.Log("ataque");
        }
    }
}
