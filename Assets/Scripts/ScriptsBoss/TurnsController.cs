using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Sprites;

public class TurnsController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    public Text hints;

    private string ActualScene;

    private string attackID;

    int HintCounter = 0;

    private string BossPhase;

    public DefenseManager defManager;

    [SerializeField]
    private GameObject battleMenu;

    private void Awake()
    {
        ActualScene = SceneManager.GetActiveScene().name;
        this.battleMenu.SetActive(false);
    }

    private void Start()
    {
        fighterStats = new List<FighterStats> ();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentFighterStats = hero.GetComponent<FighterStats> ();
        currentFighterStats.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats> ();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();
        this.battleMenu.SetActive(false);



        NextTurn();
    }

    public void NextTurn()
    {
        this.battleMenu.SetActive(false);
        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);

        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();
            if (currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);

                if (ActualScene == "BossSceneComunicacion")
                {
                    updateHintsComunication(HintCounter);
                } else if (ActualScene == "BossSceneEtica")
                {
                    updateHintsEtica(HintCounter);
                }
                else if (ActualScene == "BossSceneSeguridad")
                {
                    updateHintsSeguridad(HintCounter);
                } else
                {
                    hints.text = "Hint: oh vaya, parece que faltan hints";
                }

            } else if (defManager.Answer == "Correcto")
            {
                this.battleMenu.SetActive(true);
            }
            else if(defManager.Answer == "Incorrecto")
            {
                string attackType = Random.Range(0, 2) == 1 ? "seguridad" : "respeto";
                currentUnit.GetComponent<FighterAction>().SelectAttackBoss(attackType);
            } else
            {
                string attackType = Random.Range(0, 2) == 1 ? "seguridad" : "respeto";
                currentUnit.GetComponent<FighterAction>().SelectAttackBoss(attackType);
            }
        } else
        {
            NextTurn();
        }
    }

    public void updateHintsComunication(int hintNum)
    {
        switch (hintNum)
        {
            case 0: //outlook
                hints.text = "Hint: Es el principal medio de comunicación tanto interno, externo, como para obtener información";
                UpdateAttackID("C1");
                break;
            case 1: //rainbow
                hints.text = "Hint: Maneja las conversaciones de los distintos proyectos";
                UpdateAttackID("C2");
                break;
            case 2: //sharepoint
                hints.text = "Hint: Es donde se guarda la mayor parte de las cosas";
                UpdateAttackID("C3");
                break;
            case 3: //movil
                hints.text = "Hint: Dispositivo donde se recomienda tener las aplicaciones de comunicación descargadas";
                UpdateAttackID("C4");
                break;
            case 4: //jerarquizada
                hints.text = "Hint: Es la estructura de las comunicaciones de AWAQ";
                UpdateAttackID("C5");
                break;
            case 5: //multicultural
                hints.text = "Hint: Se colabora en un ambiente que no conoce fronteras ni nacionalidades";
                UpdateAttackID("C6");
                break;
        }

        HintCounter++;
            
    }

    public void updateHintsEtica(int hintNum)
    {
        switch (hintNum)
        {
            case 0: //Respeto
                hints.text = "Hint: Ser inclusivo y tratar a las personas dignamente";
                UpdateAttackID("E1");
                break;
            case 1: //integridad
                hints.text = "Hint: Busca un beneficio grupal, basándose en valores como la imparcialidad";
                UpdateAttackID("E2");
                break;
            case 2: //responsabilidad
                hints.text = "Hint: Manejo y conciencia de tus obligaciones, siempre teniendo un enfoque honesto";
                UpdateAttackID("E3");
                break;
            case 3: //Profesionalidad
                hints.text = "Hint: Compromiso con la calidad y excelencia mediante la mejora constante";
                UpdateAttackID("E4");
                break;
            case 4: //Compromiso
                hints.text = "Hint: Fidelidad y devoción a la organización";
                UpdateAttackID("E5");
                break;
            case 5: //transparencia
                hints.text = "Hint: Dar los resultados de manera veraz y sin ambigüedad cuando sean solicitados";
                UpdateAttackID("E6");
                break;
            case 6: //Dialogo
                hints.text = "Hint: Comunicación de conocimientos de manera efectiva, así como respetuosa";
                UpdateAttackID("E7");
                break;
            case 7: //teamwork
                hints.text = "Hint: Colaboración para un objetivo en común";
                UpdateAttackID("E8");
                break;
        }

        HintCounter++;
    }

    public void updateHintsSeguridad(int hintNum)
    {
        switch (hintNum)
        {
            case 0: //confidencialidad
                hints.text = "Hint: Acceso a la información que le corresponde a cada uno";
                UpdateAttackID("S1");
                break;
            case 1: //Contrasena
                hints.text = "Hint: Clave de seguridad la cual tiene una variedad de caracteres para evitar el acceso a mi información";
                UpdateAttackID("S2");
                break; 
            case 2: // Hackers
                hints.text = "Hint: Conjunto de técnicas para evitar caer en actividad maliciosa digital";
                UpdateAttackID("S3");
                break;
            case 3://phishing
                hints.text = "Hint: Conjunto de técnicas para detectar entidades fraudulentas";
                UpdateAttackID("S4");
                break;
        }

        HintCounter++;
    }

    public void UpdateAttackID(string _id)
    {
        attackID = _id;
    }

    public string GetAttackID()
    {
        return attackID;
    }

}
