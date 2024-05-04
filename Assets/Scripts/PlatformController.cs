using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    static public PlatformController Instance;
    public TextOverlayController uiController;

    private void Awake()
    {
        Instance = this;
        Instance.SetReferences();
    }

    void SetReferences()
    {
        if (uiController == null)
        {
            uiController = FindObjectOfType<TextOverlayController>();
        }
    }

}
