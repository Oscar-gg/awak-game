using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class DialogInstantiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogLoader.InitializeClass(FindObjectOfType<DialogController>(), GameObject.Find("ButtonExplanation").GetComponent<Button>(), Ignore);
    }

    void Ignore()
    {
       
    }

}
