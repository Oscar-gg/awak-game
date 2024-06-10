using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
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

    //Comunicacion
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

    //Seguridad
    [SerializeField]
    private GameObject ConfidencialidadPrefab;

    [SerializeField]
    private GameObject ContrasenaPrefab;

    [SerializeField]
    private GameObject HackersPrefab;

    [SerializeField]
    private GameObject PhishingPrefab;

    //Etica
    [SerializeField]
    private GameObject RespetoPrefab;

    [SerializeField]
    private GameObject IntegridadPrefab;

    [SerializeField]
    private GameObject ResponsabilidadPrefab;

    [SerializeField]
    private GameObject ProfesionalidadPrefab;

    [SerializeField]
    private GameObject CompromisoPrefab;

    [SerializeField]
    private GameObject TransparenciaPrefab;

    [SerializeField]
    private GameObject DialogoPrefab;

    [SerializeField]
    private GameObject EquipoPrefab;

    [SerializeField]
    private GameObject battleMenu;

    [SerializeField]
    private GameObject defenseMenu;

    [SerializeField]
    private GameObject wrongHint;

    //TEDI
    [SerializeField]
    private GameObject HTMLPrefab;

    [SerializeField]
    private GameObject CCSPrefab;

    [SerializeField]
    private GameObject TypeScriptPrefab;

    [SerializeField]
    private GameObject ReactPrefab;

    [SerializeField]
    private GameObject TailwindPrefab;

    [SerializeField]
    private GameObject GitHubPrefab;

    [SerializeField]
    private GameObject SCRUMPrefab;



    private string IDHint;

    private GameObject currentAttack;

    public Animator SpellAnimatorController;

    public Animator AuraAnimatorController;


    void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Start()
    {
        this.defenseMenu.SetActive(false);
        this.wrongHint.SetActive(false);
    }

    public void Update()
    {
    }

    public enum SpellsAnimations
    {
        SpellIdle, SpellUno, SpellDos, SpellTres, SpellCuatro, SpellCinco, SpellSeis,SpellSiete, SpellOcho, SpellNueve, SpellDiez, SpellOnce, SpellBossIdle, SpellBossUno, SpellBossDos
    }

    public enum AuraAnimations
    {
        AuraBoss2, AuraRojo, AuraVerde
    }


    public void SelectAttack(string btn)
    {
        IDHint = GameController.Instance.ObtainIDHint();

        UpdateSpellAnimation(SpellsAnimations.SpellIdle);
        GameObject victim = hero;
        if (tag == "Hero")
        {
            this.battleMenu.SetActive(true);
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

        }//Inicio Comuninacion
        else if (btn.CompareTo("Outlook") == 0 && IDHint == "C1")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("OutlookSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            OutlookPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueOutlook");

        }
        else if (btn.CompareTo("Mobile") == 0 && IDHint == "C4")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("MobilSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            MobilePrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueMobile");
        }
        else if (btn.CompareTo("Rainbow") == 0 && IDHint == "C2")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("RainbowSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            RainbowPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueRainbow");
        }
        else if (btn.CompareTo("Multicultural") == 0 && IDHint == "C6")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("MulticulturalSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            MulticulturalPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueMulticultural");
        }
        else if (btn.CompareTo("SharePoint") == 0 && IDHint == "C3")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("SharePointSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCinco);
            SharePointPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueSharePoint");
        }
        else if (btn.CompareTo("Jerarquia") == 0 && IDHint == "C5")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("JerarquiaSound");
            UpdateSpellAnimation(SpellsAnimations.SpellSeis);
            JerarquiaPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueJerarquia");
        }
        //Final comuniacaion
        //Inicio Seguridad
        else if (btn.CompareTo("Confidencialidad") == 0 && IDHint == "S1")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("ConfidencialidadSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            ConfidencialidadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueConfidencialidad");
        }
        else if (btn.CompareTo("Contrasena") == 0 && IDHint == "S2")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("ContrasenaSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            ContrasenaPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueContrasena");
        }
        else if (btn.CompareTo("Hackers") == 0 && IDHint == "S3")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("HackersSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            HackersPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueHackers");
        }
        else if (btn.CompareTo("Phishing") == 0 && IDHint == "S4")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("PhishingSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            PhishingPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaquePhishing");
        }
        //Final Seguridad
        //Inicio Etica
        else if (btn.CompareTo("Respeto") == 0 && IDHint == "E1")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("RespetoSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            respetoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueRespeto");
        }
        else if (btn.CompareTo("Integridad") == 0 && IDHint == "E2")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("IntegridadSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            IntegridadPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueIntegridad");
        }
        else if (btn.CompareTo("Responsabilidad") == 0 && IDHint == "E3")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("ResponsabilidadSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            respetoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueResponsabilidad");
        }
        else if (btn.CompareTo("Profesionalidad") == 0 && IDHint == "E4")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("ProfesionalidadSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            MulticulturalPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueProfesionalidad");
        }
        else if (btn.CompareTo("Compromiso") == 0 && IDHint == "E5")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("CompromisoSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            CompromisoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueCompromiso");
        }
        else if (btn.CompareTo("Transparencia") == 0 && IDHint == "E6")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("TransparenciaSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            TransparenciaPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueTransparencia");
        }
        else if (btn.CompareTo("Dialogo") == 0 && IDHint == "E7")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("DialogoSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            DialogoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueDialogo");
        }
        else if (btn.CompareTo("Equipo") == 0 && IDHint == "E8")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("EquipoSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            EquipoPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueEquipo");
        }
        //Final Etic
        //InicioTEDI
        else if (btn.CompareTo("HTML") == 0 && IDHint == "T1")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("HTMLSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            HTMLPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueHTML");
        }
        else if (btn.CompareTo("CSS") == 0 && IDHint == "T2")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("CSSSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            CCSPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueCSS");
        }
        else if (btn.CompareTo("TypeScript") == 0 && IDHint == "T3")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("TypeScriptSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            TypeScriptPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueTypeScript");
        }
        else if (btn.CompareTo("React") == 0 && IDHint == "T4")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("ReactSound");
            UpdateSpellAnimation(SpellsAnimations.SpellCuatro);
            ReactPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueReact");
        }
        else if (btn.CompareTo("Tailwind") == 0 && IDHint == "T5")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("TailwindSound");
            UpdateSpellAnimation(SpellsAnimations.SpellUno);
            TailwindPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueTailwind");
        }
        else if (btn.CompareTo("GitHub") == 0 && IDHint == "T6")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("GitHubSound");
            UpdateSpellAnimation(SpellsAnimations.SpellDos);
            GitHubPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueGitHub");
        }
        else if (btn.CompareTo("SCRUM") == 0 && IDHint == "T7")
        {
            GameController.Instance.UpdateAnaAnimation(PlayerAnimation.AnaFight);
            FindObjectOfType<BossAudioManager>().PlaySound("SCRUMSound");
            UpdateSpellAnimation(SpellsAnimations.SpellTres);
            SCRUMPrefab.GetComponent<AttackScript>().Attack(victim);
            Debug.Log("AtaqueSCRUM");
        }
        //FinalTEDI
        else
        {
            this.wrongHint.SetActive(true);
            Invoke("activateDefense", 2);
            Invoke("turnOffWrongHint", 2);
        }

        ScoreSystem.scoreValue += 100;
        Invoke("ResetAnimation", 1);
        Invoke("ResetAuraAnimation", 1);
        this.battleMenu.SetActive(false);

    }


    public void turnOffDefense()
    {
        this.defenseMenu.SetActive(false);
    }

    public void activateDefense()
    {
        this.defenseMenu.SetActive(true);
    }

    public void turnOffWrongHint()
    {
        this.wrongHint.SetActive(false);

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
        ScoreSystem.scoreValue -= 50;
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

    public void BossAuraAnimation(AuraAnimations nameAuraAnimation)
    {
        switch (nameAuraAnimation)
        {
            case AuraAnimations.AuraBoss2:
                AuraAnimatorController.SetBool("AuraBoss2", true);
                AuraAnimatorController.SetBool("AuraBossRojo", false);
                AuraAnimatorController.SetBool("AuraBossVerde", false);
                break;
            case AuraAnimations.AuraRojo:
                AuraAnimatorController.SetBool("AuraBoss2", false);
                AuraAnimatorController.SetBool("AuraBossRojo", true);
                break;
            case AuraAnimations.AuraVerde:
                AuraAnimatorController.SetBool("AuraBoss2", false);
                AuraAnimatorController.SetBool("AuraBossVerde", true);
                break;

        }
    }

    public void ResetAuraAnimation()
    {
        AuraAnimatorController.SetBool("AuraBoss2", true);
        AuraAnimatorController.SetBool("AuraBossRojo", false);
        AuraAnimatorController.SetBool("AuraBossVerde", false);
    }

    public void ReturnIdle()
    {
        UpdateSpellAnimation(SpellsAnimations.SpellIdle);
    }

}
