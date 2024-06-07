using UnityEngine;
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
