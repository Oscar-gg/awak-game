using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformController : MonoBehaviour
{

    static public PlatformController Instance;

    static public readonly string LIVES = "lives";
    static public readonly string IS_HURT = "hurt";

    // Start is called before the first frame update
    public float moveSpeed = 5;
    public float jumpForce = 5;
    public Rigidbody2D rig; // referencia la Rigidbody del jugador
    private int jumpsRemaining = 2; // Cantidad de saltos que puede realizar 
    public SpriteRenderer sr;

    public UIControllerPlatforms uiController;
    private TextOverlayController textOverlayController;

    private Dictionary<string, GameCard> cardDictionary;


    bool lookingLeft = false;

    Animator animatorController;

    Vector3 initialPosition;
    private void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt(LIVES, 3);
        Instance = this;
        Instance.SetReferences();
        initialPosition = transform.position;
        textOverlayController.HidePanelInstantly();
        PopulateDictionary();
    }

    void SetReferences()
    {
        // Para modificar propiedades de animaciones
        animatorController = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        uiController = FindObjectOfType<UIControllerPlatforms>();
        if (textOverlayController == null)
        {
            textOverlayController = FindObjectOfType<TextOverlayController>();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        // Hacer un salto cuando se presiona la flecha de arriba
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpsRemaining > 0)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Emular salto agregando vector de fuerza
                jumpsRemaining--; // Reducir cantidad de saltos disponibles
            }
        }


    }

    public void FixedUpdate()
    {
        // obtener la velocidad de las flechitas
        float xInput = Input.GetAxis("Horizontal");


        rig.velocity = new Vector2(xInput * moveSpeed, rig.velocity.y);

        // Modificar apariencia de sprite, en función del movimiento
        if (rig.velocity.x > 0)
        {
            lookingLeft = false;
        }
        else if (rig.velocity.x < 0)
        {
            lookingLeft = true;
        }

        if (lookingLeft)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (rig.velocity.y != 0)
        {
            UpdateAnimation(PlayerAnimation.jump);
        }
        else if (xInput != 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        }
        else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }

    }

    // Resetear saltos
    public void resetJump()
    {
        jumpsRemaining = 2;
    }

    // Definir animaciones
    public enum PlayerAnimation
    {
        idle, walk, jump, die, run, hurt
    }

    // Modificar los estados de la animación
    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        animatorController.SetFloat("jumpY", rig.velocity.y);

        switch (nameAnimation)
        {
            case PlayerAnimation.idle:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", false);
                break;
            case PlayerAnimation.walk:
                animatorController.SetBool("isWalking", true);
                animatorController.SetBool("isJumping", false);
                break;
            case PlayerAnimation.jump:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isJumping", true);
                break;
            case PlayerAnimation.hurt:
                animatorController.SetTrigger("isHurt");
                break;
        }
    }

    public void collidedRay()
    {
        if (PlayerPrefs.GetInt(IS_HURT, 0) == 0)
        {
            PlayerPrefs.SetInt(IS_HURT, 1);
            transform.position = initialPosition;

            int lives = PlayerPrefs.GetInt(LIVES, 0);

            PlayerPrefs.SetInt(LIVES, lives - 1);

            if (lives <= 1)
            {
                EndGame();
            }
            else
            {
                uiController.UpdateLives();

            }
            PlayerPrefs.SetInt(IS_HURT, 0);
        }

    }

    private void EndGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Fill with the data from json
    void PopulateDictionary()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/PlatformGame");

        GameCards cardJson = JsonUtility.FromJson<GameCards>(jsonFile.text);

        cardDictionary = new Dictionary<string, GameCard>();

        foreach (GameCard card in cardJson.cardInfo)
        {
            cardDictionary.Add(card.image, card);
        }
    }

    public void ShowCard(string cardName, Sprite sprite)
    {
        GameCard card;
        if (!cardDictionary.TryGetValue(cardName, out card))
        {
            card = new GameCard();
        }

        // ShowPanel(string title, string subtitle, string description, string buttonDescription)
        textOverlayController.ShowPanel(card.title, card.subtitle, card.description, "Continuar", sprite);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
