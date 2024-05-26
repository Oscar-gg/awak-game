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
        } //start1
        else if (btn.CompareTo("OutlookBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Outlook");
        }
        else if (btn.CompareTo("MobileBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Mobile");
        } //start2
        else if (btn.CompareTo("RainbowBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Rainbow");
        }
        else if (btn.CompareTo("MulticulturalBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Multicultural");
        } //start3
        else if (btn.CompareTo("SharePointBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("SharePoint");
        }
        else if (btn.CompareTo("JerarquiaBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Jerarquia");
        }


        else
        {
            Debug.Log("NormalAttack");
        }
    }

}
