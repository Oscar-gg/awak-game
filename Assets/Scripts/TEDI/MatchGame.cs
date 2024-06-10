using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
    public Sprite defaultLifeSprite; // Sprite por defecto para las vidas
    public Image[] livesImage;

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
            else
            {
                SceneManager.LoadScene("WinScene");
            }
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
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
}
