using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField] //create private y tenerlas en el inspec panel
    private bool physical;

    private GameObject hero;

    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => attachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void attachCallback(string btn)
    {
        if(btn.CompareTo("respetoBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("respeto");
        } else if (btn.CompareTo("honestidadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("honestidad");
        } else if (btn.CompareTo("seguridadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("seguridad");
        } else
        {
            Debug.Log("NormalAttack");
        }
    }

}
