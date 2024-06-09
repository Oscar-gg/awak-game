using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MatchGame : MonoBehaviour
{
    public Button[] leftButtons;
    public Button[] rightButtons;
    public Text livesText;
    public int lives = 3;
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    public Color incorrectColor = Color.red;

    private Button selectedLeftButton = null;
    private Button selectedRightButton = null;

    private Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        {"pater", "father"},
        {"mater", "mother"},
        {"filius", "son"},
        {"filia", "daughter"},
        {"Domini", "Lord"}
    };

    private void Start()
    {
        SetupButtons();
        UpdateLivesText();
    }

    private void SetupButtons()
    {
        List<string> leftWords = new List<string>(wordPairs.Keys);
        List<string> rightWords = new List<string>(wordPairs.Values);

        Shuffle(leftWords);
        Shuffle(rightWords);

        for (int i = 0; i < leftButtons.Length; i++)
        {
            leftButtons[i].GetComponentInChildren<Text>().text = leftWords[i];
            leftButtons[i].onClick.AddListener(() => OnLeftButtonClick(leftButtons[i]));
            leftButtons[i].image.color = defaultColor;
        }

        for (int i = 0; i < rightButtons.Length; i++)
        {
            rightButtons[i].GetComponentInChildren<Text>().text = rightWords[i];
            rightButtons[i].onClick.AddListener(() => OnRightButtonClick(rightButtons[i]));
            rightButtons[i].image.color = defaultColor;
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

        if (wordPairs[leftText.text] == rightText.text)
        {
            // Correct match
            selectedLeftButton.interactable = false;
            selectedRightButton.interactable = false;
            selectedLeftButton.image.color = Color.green;
            selectedRightButton.image.color = Color.green;
        }
        else
        {
            // Incorrect match
            StartCoroutine(IncorrectMatch());
            lives--;
            UpdateLivesText();
        }

        selectedLeftButton = null;
        selectedRightButton = null;

        if (CheckWinCondition())
        {
            SceneManager.LoadScene("WinScene");
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

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
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
