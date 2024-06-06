using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class TextOverlayController : MonoBehaviour
{

    CanvasGroup CanvasGroup;
    GameObject textPanel;

    Button panelButton;
    Text title;
    Text subtitle;
    Text description;
    Text buttonDescription;
    Image renderImage;
    Action buttonRes;


    public void ShowPanel(string title, string subtitle, string description, string buttonDescription, Sprite sp)
    {
        buttonRes = IgnoreAction;
        //Debug.Log(sprite.name);
        renderImage.overrideSprite = sp;
        
        this.title.text = title;
        this.subtitle.text = subtitle;
        this.description.text = description;
        this.buttonDescription.text = buttonDescription;
        StartCoroutine(Fade(0.025f, 1, 0.05f, true));
    }

    public void ShowPanel(string title, string subtitle, string description, string buttonDescription, Sprite sp, Action action)
    {
        buttonRes = action;
        //Debug.Log(sprite.name);
        renderImage.overrideSprite = sp;

        this.title.text = title;
        this.subtitle.text = subtitle;
        this.description.text = description;
        this.buttonDescription.text = buttonDescription;
        StartCoroutine(Fade(0.025f, 1, 0.025f, true));
    }

    // Private to only allow self to hide
    public void HidePanel()
    {
        StartCoroutine(Fade(0.025f, 0, 0.025f, false));
        buttonRes();
    }

    public void HidePanelInstantly()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
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
        title = GameObject.Find("TextTitle").GetComponent<Text>();
        subtitle = GameObject.Find("TextSubtitle").GetComponent<Text>(); ;
        description = GameObject.Find("TextExplanation").GetComponent<Text>(); ;
        renderImage = GameObject.Find("ImagePanel").GetComponent<Image>();
        //description = textPanel.transform.GetChild(4).gameObject.GetComponent<Text>(); ;

        panelButton = textPanel.GetComponentInChildren<Button>();
        buttonDescription = panelButton.GetComponentInChildren<Text>();

        // Attach fade out callback to button
        panelButton.onClick.AddListener(() => HidePanel());
    }

    void IgnoreAction()
    {

    }

}
