using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerControllerAna;


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

    [SerializeField]
    private GameObject OutlookPrefab;

    [SerializeField]
    private GameObject MobilePrefab;

    [SerializeField]
    private GameObject RainbowPrefab;

    [SerializeField]
    private GameObject MulticulturalPrefab;

    [SerializeField]
    private GameObject SharePointPrefab;

    [SerializeField]
    private GameObject JerarquiaPrefab;

    private GameObject currentAttack;

    public Animator SpellAnimatorController;


    void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    public enum SpellsAnimations
    {
        SpellIdle, SpellUno, SpellDos, SpellTres, SpellCuatro, SpellCinco, SpellSeis,SpellSiete, SpellOcho, SpellNueve, SpellDiez, SpellOnce, SpellBossIdle, SpellBossUno, SpellBossDos
    }


    public void SelectAttack(string btn)
    {
        UpdateSpellAnimation(SpellsAnimations.SpellIdle);
        GameObject victim = hero;
        if (tag == "Hero")
        {
            victim = enemy;
        }
        if (btn.CompareTo("respeto") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound(""); //para poder usar el audio, se tienen que poner el nombre que se le dio en unity
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            respetoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueRespeto");

        }
        else if (btn.CompareTo("honestidad") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            honestidadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueHonestidad");


        }
        else if (btn.CompareTo("seguridad") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            seguridadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueSeguridad");

        }//start
        else if (btn.CompareTo("Outlook") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("OutlookSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            OutlookPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueOutlook");

        }
        else if (btn.CompareTo("Mobile") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("MobilSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            MobilePrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueMobile");

        }//start2
        else if (btn.CompareTo("Rainbow") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("RainbowSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            RainbowPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueRainbow");

        }
        else if (btn.CompareTo("Multicultural") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("MulticulturalSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            MulticulturalPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueMulticultural");

        }//start3
        else if (btn.CompareTo("SharePoint") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("SharePointSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCinco);
            SharePointPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueSharePoint");

        }
        else if (btn.CompareTo("Jerarquia") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("JerarquiaSound");
            UpdateSpellAnimation(SpellsAnimations.SpellSeis);
            JerarquiaPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueJerarquia");

        }

        else
        {
            Debug.Log("ataque");
        }

        Invoke("ResetAnimation", 1);
       
    }

    public void SelectAttackBoss(string btn)
    {
        UpdateSpellBossAnimation(SpellsAnimations.SpellIdle);
        GameObject victim = hero;
        if (tag == "Hero")
        {
            victim = enemy;
        }
        if (btn.CompareTo("respeto") == 0)
        {
            UpdateSpellBossAnimation(SpellsAnimations.SpellBossUno);
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaHurt);
            FindObjectOfType<BossAudioManager>().PlaySound("BossAttackUno");
            respetoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueBossRespeto");

        }
        else if (btn.CompareTo("honestidad") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaHurt);
            UpdateSpellBossAnimation(SpellsAnimations.SpellBossDos);
            honestidadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueBossHonestidad");

        }
        else if (btn.CompareTo("seguridad") == 0)
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaHurt);
            UpdateSpellBossAnimation(SpellsAnimations.SpellBossDos);
            FindObjectOfType<BossAudioManager>().PlaySound("BossAttackDos");
            seguridadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueBossSeguridad");

        }
        else
        {
            Debug.Log("ataque");
        }

        Invoke("BossAnimation", 1);

    }

    public void UpdateSpellAnimation(SpellsAnimations nameSpellAnimatino)
    {
        switch(nameSpellAnimatino)
        {
            case SpellsAnimations.SpellIdle:
                SpellAnimatorController.SetBool("SpellIdle", true);
                SpellAnimatorController.SetBool("SpellUno", false);
                SpellAnimatorController.SetBool("SpellDos", false);
                SpellAnimatorController.SetBool("SpellTres", false);
                SpellAnimatorController.SetBool("SpellCuatro", false);
                SpellAnimatorController.SetBool("SpellCinco", false);
                SpellAnimatorController.SetBool("SpellSeis", false);
                SpellAnimatorController.SetBool("SpellSiete", false); 
                SpellAnimatorController.SetBool("SpellOcho", false);
                SpellAnimatorController.SetBool("SpellNueve", false);
                SpellAnimatorController.SetBool("SpellDiez", false);
                SpellAnimatorController.SetBool("SpellOnce", false);
                break;
            case SpellsAnimations.SpellUno:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellUno", true);
                break;
            case SpellsAnimations.SpellDos:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellDos", true);
                break;
            case SpellsAnimations.SpellTres:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellTres", true);
                break;
            case SpellsAnimations.SpellCuatro:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellCuatro", true);
                break;
            case SpellsAnimations.SpellCinco:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellCinco", true);
                break;
            case SpellsAnimations.SpellSeis:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellSeis", true);
                break;
            case SpellsAnimations.SpellSiete:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellSiete", true);
                break;
            case SpellsAnimations.SpellOcho:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellOcho", true);
                break;
            case SpellsAnimations.SpellNueve:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellNueve", true);
                break;
            case SpellsAnimations.SpellDiez:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellDiez", true);
                break;
            case SpellsAnimations.SpellOnce:
                SpellAnimatorController.SetBool("SpellIdle", false);
                SpellAnimatorController.SetBool("SpellOnce", true);
                break;
        }
    }

    public void UpdateSpellBossAnimation(SpellsAnimations nameSpellAnimatino)
    {
        switch (nameSpellAnimatino)
        {
            case SpellsAnimations.SpellBossIdle:
                SpellAnimatorController.SetBool("SpellBossIdle", true);
                SpellAnimatorController.SetBool("SpellBossUno", false);
                SpellAnimatorController.SetBool("SpellBossDos", false);
                break;
            case SpellsAnimations.SpellBossUno:
                SpellAnimatorController.SetBool("SpellBossIdle", false);
                SpellAnimatorController.SetBool("SpellBossUno", true);
                break;
            case SpellsAnimations.SpellBossDos:
                SpellAnimatorController.SetBool("SpellBossIdle", false);
                SpellAnimatorController.SetBool("SpellBossDos", true);
                break;
        }
    }

    public void ResetAnimation()
    {
        SpellAnimatorController.SetBool("SpellIdle", true);
        SpellAnimatorController.SetBool("SpellUno", false);
        SpellAnimatorController.SetBool("SpellDos", false);
        SpellAnimatorController.SetBool("SpellTres", false);
        SpellAnimatorController.SetBool("SpellCuatro", false);
        SpellAnimatorController.SetBool("SpellCinco", false);
        SpellAnimatorController.SetBool("SpellSeis", false);
        SpellAnimatorController.SetBool("SpellSiete", false);
        SpellAnimatorController.SetBool("SpellOcho", false);
        SpellAnimatorController.SetBool("SpellNueve", false);
        SpellAnimatorController.SetBool("SpellDiez", false);
        SpellAnimatorController.SetBool("SpellOnce", false);
    }

    public void BossAnimation()
    {
        SpellAnimatorController.SetBool("SpellBossIdle", true);
        SpellAnimatorController.SetBool("SpellBossUno", false);
        SpellAnimatorController.SetBool("SpellBossDos", false);
    }

    public void ReturnIdle()
    {
        UpdateSpellAnimation(SpellsAnimations.SpellIdle);
    }

}
