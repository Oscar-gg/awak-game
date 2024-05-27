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
        } 
        //Inicio Comuninacion
        else if (btn.CompareTo("OutlookBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Outlook");
        }
        else if (btn.CompareTo("MobileBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Mobile");
        } 
        else if (btn.CompareTo("RainbowBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Rainbow");
        }
        else if (btn.CompareTo("MulticulturalBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Multicultural");
        } 
        else if (btn.CompareTo("SharePointBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("SharePoint");
        }
        else if (btn.CompareTo("JerarquiaBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Jerarquia");
        } 
        //Final comuniacaion
        //Inicio Seguridad
        else if (btn.CompareTo("ConfidencialidadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Confidencialidad");
        }
        else if (btn.CompareTo("ContrasenaBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Contrasena");
        }
        else if (btn.CompareTo("HackersBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Hackers");
        }
        else if (btn.CompareTo("PhishingBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Phishing");
        }
        //Final seguridad
        //Inicio Etica
        else if (btn.CompareTo("RespetoBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Respeto");
        }
        else if (btn.CompareTo("IntegridadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Integridad");
        }
        else if (btn.CompareTo("ResponsabilidadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Responsabilidad");
        }
        else if (btn.CompareTo("ProfesionalidadBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Profesionalidad");
        }
        else if (btn.CompareTo("CompromisoBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Compromiso");
        }
        else if (btn.CompareTo("TransparenciaBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Transparencia");
        }
        else if (btn.CompareTo("DialogoBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Dalogo");
        }
        else if (btn.CompareTo("EquipoBtn") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("Equipo");
        }
        //Final Etica

        else
        {
            Debug.Log("NormalAttack");
        }
    }

}
