using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class TurnsController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    public Text hints;

    int HintCounter = 0;

    [SerializeField]
    private GameObject battleMenu;

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
            if(currentUnit.tag == "Hero")
            {
                this.battleMenu.SetActive(true);
                updateHintsComunication(HintCounter);

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
                hints.text = "Hint: Es el principal medio de comunicación tanto interno, externo, como para obtener información";
                break;
            case 1:
                hints.text = "Hint: Maneja las conversaciones de los distintos proyectos";
                break;
            case 2:
                hints.text = "Hint: Es donde se guarda la mayor parte de las cosas";
                break;
            case 3:
                hints.text = "Hint: Dispositivo donde se recomienda tener las aplicaciones de comunicación descargadas";
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

}
