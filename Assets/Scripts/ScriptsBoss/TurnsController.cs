using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnsController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    public Text hints;

    private string ActualScene;

    int HintCounter = 0;

    [SerializeField]
    private GameObject battleMenu;

    private void Awake()
    {
        ActualScene = SceneManager.GetActiveScene().name;
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
            case 0:
                hints.text = "Hint: Es el principal medio de comunicaci�n tanto interno, externo, como para obtener informaci�n";
                break;
            case 1:
                hints.text = "Hint: Maneja las conversaciones de los distintos proyectos";
                break;
            case 2:
                hints.text = "Hint: Es donde se guarda la mayor parte de las cosas";
                break;
            case 3:
                hints.text = "Hint: Dispositivo donde se recomienda tener las aplicaciones de comunicaci�n descargadas";
                break;
            case 4:
                hints.text = "Hint: Es la estructura de las comunicaciones de AWAQ";
                break;
            case 5:
                hints.text = "Hint: Se colabora en un ambiente que no conoce fronteras ni nacionalidades";
                break;
        }

        HintCounter++;
            
    }

    public void updateHintsEtica(int hintNum)
    {
        switch (hintNum)
        {
            case 0:
                hints.text = "Hint: Ser inclusivo y tratar a las personas dignamente";
                break;
            case 1:
                hints.text = "Hint: Busca un beneficio grupal, bas�ndose en valores como la imparcialidad";
                break;
            case 2:
                hints.text = "Hint: Manejo y conciencia de tus obligaciones, siempre teniendo un enfoque honesto";
                break;
            case 3:
                hints.text = "Hint: Compromiso con la calidad y excelencia mediante la mejora constante";
                break;
            case 4:
                hints.text = "Hint: Fidelidad y devoci�n a la organizaci�n";
                break;
            case 5:
                hints.text = "Hint: Dar los resultados de manera veraz y sin ambig�edad cuando sean solicitados";
                break;
            case 6:
                hints.text = "Hint: Comunicaci�n de conocimientos de manera efectiva, as� como respetuosa";
                break;
            case 7:
                hints.text = "Hint: Colaboraci�n para un objetivo en com�n";
                break;
        }

        HintCounter++;
    }

    public void updateHintsSeguridad(int hintNum)
    {
        switch (hintNum)
        {
            case 0:
                hints.text = "Hint: Acceso a la informaci�n que le corresponde a cada uno";
                break;
            case 1:
                hints.text = "Hint: Clave de seguridad la cual tiene una variedad de caracteres para evitar el acceso a mi informaci�n";
                break;
            case 2:
                hints.text = "Hint: Conjunto de t�cnicas para evitar caer en actividad maliciosa digital";
                break;
            case 3:
                hints.text = "Hint: Conjunto de t�cnicas para detectar entidades fraudulentas";
                break;
        }

        HintCounter++;
    }


}
