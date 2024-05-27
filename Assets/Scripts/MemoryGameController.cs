using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class MemoryGameController : MonoBehaviour
{

    static public MemoryGameController Instance;
    public TextOverlayController uiController;
    public MemoryGameUI memoryGameUI;

    [SerializeField]
    private Sprite skill;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> cards = new List<Button>();

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private Dictionary<string, GameCard> cardDictionary;


    private void Awake()
    {
        Instance = this;
        Instance.SetReferences();

        puzzles = Resources.LoadAll<Sprite>("Sprites/memoryGame");
        PopulateDictionary();
    }

    void SetReferences()
    {
        if (uiController == null)
        {
            uiController = FindObjectOfType<TextOverlayController>();
        }
        if (memoryGameUI == null)
        {
            memoryGameUI = FindAnyObjectByType<MemoryGameUI>();
        }
    }

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;

        memoryGameUI.SetText(0, gameGuesses);
        uiController.HidePanelInstantly();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MemoryCard");
        for(int i = 0; i < objects.Length; i++)
        {
            cards.Add(objects[i].GetComponent<Button>());
            cards[i].image.sprite = skill;
        }
    }

    void AddListeners()
    {
        foreach (Button button in cards)
        {
            button.onClick.AddListener(() => PickCard());
        }
    }

    public void PickCard()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {

            firstGuess = true;

            FindObjectOfType<BossAudioManager>().PlaySound("flip");


            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            StartCoroutine(cards[firstGuessIndex].gameObject.GetComponent<Rotate>().FlipCard(gamePuzzles[firstGuessIndex]));

        } else if (!secondGuess)
        {
            FindObjectOfType<BossAudioManager>().PlaySound("flip");

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            
            if (secondGuessIndex == firstGuessIndex)
                return;
            

            secondGuess = true;
            
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            
            StartCoroutine(cards[secondGuessIndex].gameObject.GetComponent<Rotate>().FlipCard(gamePuzzles[secondGuessIndex]));

            StartCoroutine(CheckIfPuzzlesMatch());
        }

    }

    void AddGamePuzzles()
    {
        int looper = cards.Count;
        int index = 0;
        for(int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    IEnumerator CheckIfPuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            FindObjectOfType<BossAudioManager>().PlaySound("match");

            yield return new WaitForSeconds(0.5f);
            
            cards[firstGuessIndex].interactable = false;
            cards[secondGuessIndex].interactable = false;

            cards[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            cards[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            memoryGameUI.SetText(countCorrectGuesses+1, gameGuesses);

            yield return new WaitForSeconds(1.5f);

            int powerIndex = int.Parse(cards[firstGuessIndex].name);

            GameCard card;
            if (!cardDictionary.TryGetValue(firstGuessPuzzle, out card))
            {
                card = new GameCard();
            }

            // ShowPanel(string title, string subtitle, string description, string buttonDescription)
            uiController.ShowPanel(card.title, card.subtitle, card.description, "Continuar", cards[firstGuessIndex].image.sprite);


            CheckGameFinished();
        } else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(cards[firstGuessIndex].gameObject.GetComponent<Rotate>().FlipCard(skill));
            //cards[firstGuessIndex].image.sprite = skill;
            StartCoroutine(cards[secondGuessIndex].gameObject.GetComponent<Rotate>().FlipCard(skill));
            //cards[secondGuessIndex].image.sprite = skill;
        }
        yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;
    }

    void CheckGameFinished()
    {
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game finished");
            Debug.Log("It took you " + countGuesses + " attempts to finish the game");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Fill with the data from json
    void PopulateDictionary()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/MemoryGame");

        GameCards cardJson = JsonUtility.FromJson<GameCards>(jsonFile.text);

        cardDictionary = new Dictionary<string, GameCard>();

        foreach (GameCard card in cardJson.cardInfo)
        {
            cardDictionary.Add(card.image, card);
        }
    }

}
