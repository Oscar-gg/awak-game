using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MemoryGameUI : MonoBehaviour
{

    private Text foundText;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject find = GameObject.Find("CountPoderes");

        foundText = find.GetComponent<Text>();
    }

    public void SetText(int found, int total)
    {
        foundText.text = "" + found + "/" + total;
    }
}
