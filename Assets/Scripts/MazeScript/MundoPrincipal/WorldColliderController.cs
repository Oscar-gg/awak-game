using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Este script es para poder desbloquear los niveles 


public class WorldColliderController : MonoBehaviour
{
    public string minigameName;
    public string worldScene;
    public string popupMessage;

    private Sprite lockedImage; // Objeto de UI que muestra el candado cerrado
    private Sprite unlockedImage;
    private SpriteRenderer background;
    bool unlockedWorld;
    PopupGeneric genericPopup;


    private void Awake()
    {
        lockedImage = Resources.Load<Sprite>("Sprites/lock");
        unlockedImage = Resources.Load<Sprite>("Sprites/unlock");
        SpriteRenderer[] all = gameObject.GetComponentsInChildren<SpriteRenderer>();
        background = all[1];
        Debug.Log(all.Length);
        genericPopup = FindObjectOfType<PopupGeneric>();
    }

    private void Start()
    {
        unlockedWorld = PlayerProgress.Instance.CanAccessMiniGame(minigameName);

        // Actualizar el estado inicial del mundo
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (unlockedWorld)
        {
            GetComponent<SpriteRenderer>().sprite = unlockedImage;
            background.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = lockedImage;
            background.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (unlockedWorld && other.CompareTag("Player"))
        {
            genericPopup.ShowPopup(popupMessage, worldScene);
        }
    }
}
