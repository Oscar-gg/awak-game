using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    CanvasGroup CanvasGroup;
    GameObject textPanel;

    Button panelButton;
    Text title;
    Text subtitle;
    Text description;
    Text buttonDescription;


    public void ShowPanel(string title, string subtitle, string description, string buttonDescription)
    {
        this.title.text = title;
        this.subtitle.text = subtitle;
        this.description.text = description;
        this.buttonDescription.text = buttonDescription;
        StartCoroutine(Fade(0.1f, 1, 0.2f, true));
    }

    // Private to only allow self to hide
    private void HidePanel()
    {
        StartCoroutine(Fade(0.1f, 0, 0.2f, false));
    }

    IEnumerator Fade(float alphaStep, float target, float timeStep, bool increase)
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
        } else
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
    }


    void Awake()
    {
        // Find reference of panel
        textPanel = GameObject.FindGameObjectWithTag("Explanation");
        CanvasGroup = textPanel.GetComponent<CanvasGroup>();

        title = textPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
        subtitle = textPanel.transform.GetChild(2).gameObject.GetComponent<Text>(); ;
        description = textPanel.transform.GetChild(3).gameObject.GetComponent<Text>(); ;

        panelButton = textPanel.GetComponentInChildren<Button>();
        buttonDescription = panelButton.GetComponentInChildren<Text>();

        // Attach fade out callback to button
        panelButton.onClick.AddListener(() => HidePanel());
    }

}
