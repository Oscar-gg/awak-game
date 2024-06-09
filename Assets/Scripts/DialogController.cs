using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    Image characterTalking;
    Text dialog;
    Text characterName;
    Text nextText;
    Button nextButton;
    Button previousButton;
    CanvasGroup CanvasGroup;
    string[] dialogs;
    Action endAction;

    Coroutine dialogCoro;

    int currentDialog = 0;
    
    float DELAY_TO_WRITE = 0.5f;
    float DIALOG_SPEED = 0.05f;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        characterTalking = GameObject.Find("CharacterTalking").GetComponent<Image>();
        dialog = GameObject.Find("DialogText").GetComponent<Text>();
        characterName = GameObject.Find("NameText").GetComponent<Text>();
        
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextText = nextButton.gameObject.GetComponentInChildren<Text>();

        previousButton = GameObject.Find("PreviousButton").GetComponent<Button>();

        previousButton.onClick.AddListener(() => ClickedPrevious());
        nextButton.onClick.AddListener(() => ClickedNext());

        HideDialogInstantly();
    }

    public void ShowPanel(string character, string[] dialogs, Sprite sp, Action action)
    {
        characterTalking.overrideSprite = sp;
        characterName.text = character;
        endAction = action;
        this.dialogs = dialogs;
        currentDialog = 0;
        this.dialog.text = "";

        if (dialogs.Length > 1)
        {
            nextText.text = "Siguiente";
        } else
        {
            nextText.text = "Finalizar";
        }
       

        PreviousButton(false);
        StartCoroutine(Fade(0.025f, 1, 0.05f, true, true));
    }

    // Private to only allow self to hide
    public void HidePanel()
    {
        StartCoroutine(Fade(0.025f, 0, 0.05f, false));
    }

    public void HideDialogInstantly()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    IEnumerator Fade(float alphaStep, float target, float timeStep, bool increase, bool setText=false)
    {
        if (increase)
        {
            CanvasGroup.interactable = true;
            while (CanvasGroup.alpha < target)
            {
                CanvasGroup.alpha += alphaStep;
                yield return new WaitForSeconds(timeStep);
            }
            CanvasGroup.alpha = target;
            CanvasGroup.blocksRaycasts = true;
        }
        else
        {
            while (CanvasGroup.alpha > target)
            {
                CanvasGroup.alpha -= alphaStep;
                yield return new WaitForSeconds(timeStep);
            }
            CanvasGroup.alpha = target;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }

        if (setText)
        {
            dialogCoro = StartCoroutine(UpdateText());
        }
    }

    private void ClickedPrevious()
    {
        currentDialog--;
        currentDialog = Mathf.Max(currentDialog, 0);
        UpdatePreviousButton();
        StartDialogCoro();
    }

    private void ClickedNext()
    {
        currentDialog++;
        currentDialog = Mathf.Min(currentDialog, dialogs.Length);
        UpdatePreviousButton();

        // Finalizar
        if (currentDialog == dialogs.Length)
        {
            HidePanel();
            endAction();
            return;
        }

        if (currentDialog == dialogs.Length - 1)
        {
            nextText.text = "Finalizar";
        } else {
            nextText.text = "Siguiente";
        }

        StartDialogCoro();
    }

    private void UpdatePreviousButton()
    {
        PreviousButton(currentDialog > 0);
    }

    private void PreviousButton(bool on)
    {
        if (on)
        {
            previousButton.gameObject.SetActive(true);
        } else
        {
            previousButton.gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateText()
    {
        dialog.text = "";
        
        yield return new WaitForSeconds(DELAY_TO_WRITE);
           
        if (currentDialog < dialogs.Length) {
            foreach (char Character in dialogs[currentDialog])
            {
                dialog.text += Character;
                yield return new WaitForSeconds(DIALOG_SPEED);
            }
        }
        
        
        dialogCoro = null;
        
    }

    void StartDialogCoro()
    {
        if (dialogCoro != null)
        {
            StopCoroutine(dialogCoro);
        }

        dialogCoro = StartCoroutine(UpdateText());
    }
}
