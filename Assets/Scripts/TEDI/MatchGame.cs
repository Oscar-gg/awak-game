using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine.U2D;

public class MatchGame : MonoBehaviour
{
    public Button[] leftButtons;
    public Button[] rightButtons;
    // public Text livesText;
    public Text dictionaryText;
    public int lives = 5;
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    public Color incorrectColor = Color.red;

    private Button selectedLeftButton = null;
    private Button selectedRightButton = null;
    private int currentSetIndex = 0;
    private const int totalSets = 7;

    public Sprite spendLives;
    public Sprite defaultLifeSprite;
    public Image[] livesImage;

    private Coroutine timeCoro;
    private int playTime = 0;

    public TextOverlayController uiController;
    private Dictionary<string, GameCard> cardDictionary;
    private Dictionary<string, Sprite> spritesDictionary;


    private List<Dictionary<string, string>> wordSets = new List<Dictionary<string, string>>()
    {
        new Dictionary<string, string>()
        {
            {"<h1>", "Encabezado importante"},
            {"<p>", "Párrafo"},
            {"<img>", "Imagen"},
            {"<a>", "Hipervínculo"},
            {"<div>", "División"}
        },
        new Dictionary<string, string>()
        {
            {"color", "Color del texto"},
            {"margin", "Margen del elemento"},
            {"padding", "Relleno del elemento"},
            {"font-size", "Tamaño de fuente"},
            {"background-color", "Color de fondo"}
        },
        new Dictionary<string, string>()
        {
            {"let", "Variable local"},
            {"const", "Constante local"},
            {"=>", "Función flecha"},
            {"interface", "Interfaz de TypeScript"},
            {"type", "Alias de tipo"}
        },
        new Dictionary<string, string>()
        {
            {"useState", "Hook de estado"},
            {"useEffect", "Hook de efectos"},
            {"Component", "Clase base de componentes"},
            {"JSX", "Sintaxis JavaScript XML"},
            {"props", "Propiedades de componentes"}
        },
        new Dictionary<string, string>()
        {
            {"flex", "Contexto de flexbox"},
            {"justify-center", "Centrar horizontalmente"},
            {"bg-blue-500", "Fondo azul"},
            {"text-xl", "Texto extra grande"},
            {"p-4", "Relleno de 1rem"}
        },
        new Dictionary<string, string>()
        {
            {"git init", "Inicializa repositorio"},
            {"git commit", "Guarda cambios"},
            {"git push", "Sube cambios"},
            {"git pull", "Actualiza repositorio"},
            {"git clone", "Copia repositorio"}
        },
        new Dictionary<string, string>()
        {
            {"Product Backlog", "Lista de tareas"},
            {"Sprint", "Iteración de trabajo"},
            {"Daily Scrum", "Reunión diaria"},
            {"Sprint Review", "Revisión de sprint"},
            {"Scrum Master", "Facilitador del equipo"}
        }
    };

    private string[] dictionaryTitles = new string[]
    {
        "HTML",
        "CSS",
        "TypeScript",
        "React",
        "Tailwind CSS",
        "GitHub",
        "SCRUM"
    };

    private void Start()
    {
        SetupButtons();
        // UpdateLivesText();
        UpdateDictionaryText();
        UpdateLivesImage();
        playTime = 0;
        timeCoro = StartCoroutine(ContarTiempo());
        uiController.HidePanelInstantly();

    }

    private void Awake()
    {
        if (uiController == null)
        {
            uiController = FindObjectOfType<TextOverlayController>();
        }
        PopulateDictionary();
        PopulateSprites();
    }

    private void SetupButtons()
    {
        Dictionary<string, string> wordPairs = wordSets[currentSetIndex];

        List<string> leftWords = new List<string>(wordPairs.Keys);
        List<string> rightWords = new List<string>(wordPairs.Values);

        Shuffle(leftWords);
        Shuffle(rightWords);

        for (int i = 0; i < leftButtons.Length; i++)
        {
            leftButtons[i].GetComponentInChildren<Text>().text = leftWords[i];
            leftButtons[i].onClick.AddListener(() => OnLeftButtonClick(leftButtons[i]));
            leftButtons[i].image.color = defaultColor;
            leftButtons[i].interactable = true;
        }

        for (int i = 0; i < rightButtons.Length; i++)
        {
            rightButtons[i].GetComponentInChildren<Text>().text = rightWords[i];
            rightButtons[i].onClick.AddListener(() => OnRightButtonClick(rightButtons[i]));
            rightButtons[i].image.color = defaultColor;
            rightButtons[i].interactable = true;
        }
    }

    public void OnLeftButtonClick(Button button)
    {
        if (selectedLeftButton != null)
        {
            selectedLeftButton.image.color = defaultColor;
        }

        selectedLeftButton = button;
        selectedLeftButton.image.color = selectedColor;

        if (selectedRightButton != null)
        {
            CheckMatch();
        }
    }

    public void OnRightButtonClick(Button button)
    {
        if (selectedRightButton != null)
        {
            selectedRightButton.image.color = defaultColor;
        }

        selectedRightButton = button;
        selectedRightButton.image.color = selectedColor;

        if (selectedLeftButton != null)
        {
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        Text leftText = selectedLeftButton.GetComponentInChildren<Text>();
        Text rightText = selectedRightButton.GetComponentInChildren<Text>();

        Dictionary<string, string> wordPairs = wordSets[currentSetIndex];

        if (wordPairs[leftText.text] == rightText.text)
        {
            selectedLeftButton.interactable = false;
            selectedRightButton.interactable = false;
            selectedLeftButton.image.color = Color.green;
            selectedRightButton.image.color = Color.green;
        }
        else
        {
            StartCoroutine(IncorrectMatch());
            lives--;
            // UpdateLivesText();
            UpdateLivesImage();
        }

        selectedLeftButton = null;
        selectedRightButton = null;

        if (CheckWinCondition())
        {
            currentSetIndex++;

            ShowPanel(dictionaryTitles[currentSetIndex-1]);

            if (currentSetIndex < totalSets)
            {
                if (lives < 5)
                {
                    lives++;
                    // UpdateLivesText();
                    UpdateLivesImage();
                }
                UpdateDictionaryText();
                SetupButtons();
            }
        }

        if (lives <= 0)
        {
            EndGame();
        }
    }

    private IEnumerator IncorrectMatch()
    {
        selectedLeftButton.image.color = incorrectColor;
        selectedRightButton.image.color = incorrectColor;

        yield return new WaitForSeconds(1);

        selectedLeftButton.image.color = defaultColor;
        selectedRightButton.image.color = defaultColor;
    }

    /* private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
    } */

    public void UpdateLivesImage()
    {
        for (int i = 0; i < livesImage.Length; i++)
        {
            if (i < lives)
            {
                livesImage[i].sprite = defaultLifeSprite;
            }
            else
            {
                livesImage[i].sprite = spendLives;
            }
        }
    }

    private void UpdateDictionaryText()
    {
        if (dictionaryText != null)
        {
            dictionaryText.text = dictionaryTitles[currentSetIndex];
        }
    }

    private bool CheckWinCondition()
    {
        foreach (Button button in leftButtons)
        {
            if (button.interactable)
            {
                return false;
            }
        }
        return true;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private void EndGame()
    {
        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneNames.LOSE_G);
    }

    private IEnumerator WinFinishRoutine()
    {
        StopCoroutine(timeCoro);

        yield return PlayerProgress.Instance.UpdateProgess(MiniGameNames.ASSOCIATION, 1000 + lives * 100, playTime);

        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneNames.WIN_G);
    }

    private IEnumerator ContarTiempo()
    {
        while (true)
        {
            playTime++;
            yield return new WaitForSeconds(1);
        }
    }

    private void PopulateDictionary()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/TEDI_Game");

        GameCards cardJson = JsonUtility.FromJson<GameCards>(jsonFile.text);

        cardDictionary = new Dictionary<string, GameCard>();

        foreach (GameCard card in cardJson.cardInfo)
        {
            cardDictionary.Add(card.image, card);
        }
    }

    private void PopulateSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/TEDI_Minigame");
        spritesDictionary = new Dictionary<string, Sprite>();

        foreach (Sprite sprite in sprites)
        {
            spritesDictionary.Add(sprite.name, sprite);
        }
    }

    private void ShowPanel(string power)
    {
        GameCard card;
        Sprite sp;

        Debug.Log("Power: " + power);

        if (!cardDictionary.TryGetValue(power, out card))
        {
            card = new GameCard();
        }

        Debug.Log("IMAGEN SPRITE");
        Debug.Log(spritesDictionary[power]);

        if (!spritesDictionary.TryGetValue(power, out sp))
        {
            sp = spendLives;
        }

        uiController.ShowPanel(card.title, card.subtitle, card.description, "Continuar", sp, EndIfWin);
    }

    private void EndIfWin()
    {
        if (currentSetIndex >= totalSets)
        {
            StartCoroutine(WinFinishRoutine());
        }
    }
}