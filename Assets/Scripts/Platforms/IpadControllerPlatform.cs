using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IpadControllerPlatform : MonoBehaviour
{
    bool moveLeft;
    bool moveRight;
    bool moveUp;

    static public IpadControllerPlatform Instance;

    static public readonly string LIVES = "lives";
    static public readonly string IS_HURT = "hurt";

    private int TOTAL_CARDS = 4;
    private int currentCards = 0;

    public float moveSpeed = 5;
    public float jumpForce = 5;
    public Rigidbody2D rig;
    private int jumpsRemaining = 2;
    public SpriteRenderer sr;

    public UIControllerPlatforms uiController;
    private TextOverlayController textOverlayController;

    private Dictionary<string, GameCard> cardDictionary;

    bool lookingLeft = false;

    Animator animatorController;

    Vector3 initialPosition;

    public GameProgressController gameProgressController;
    public string nextLevelName;

    private int tiempojugado;
    private Coroutine timeCoro;

    private void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt(LIVES, 3);
        Instance = this;
        Instance.SetReferences();
        initialPosition = transform.position;
        textOverlayController.HidePanelInstantly();
        PopulateDictionary();
        currentCards = 0;
        tiempojugado = 0;
        SetGameProgressController(GameObject.FindObjectOfType<GameProgressController>());
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }
    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }
    public void PointerUpRight()
    {
        moveRight = false;
    }

    public void PointerDownUp()
    {
        moveUp = true;
    }
    public void PointerUpUp()
    {
        moveUp = false;
    }

    public void SetGameProgressController(GameProgressController progressController)
    {
        if (progressController != null)
        {
            gameProgressController = progressController;
        }
        else
        {
            Debug.LogWarning("GameProgressController recibido es nulo");
        }
    }

    void SetReferences()
    {
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

        timeCoro = StartCoroutine(ContarTiempo());
        // Make sure db is initialized
        PlayerProgress.Instance.IsValidUser();
    }

    void Update()
    {
        if (moveUp)
        {
            if (jumpsRemaining > 0)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                FindObjectOfType<BossAudioManager>().PlaySound("Jump");
                jumpsRemaining--;
                moveUp = false; 
            }
        }

        if (rig.velocity.y == 0 && !moveUp)
        {
            resetJump();
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = 0f;
        if (moveLeft)
        {
            horizontalInput = -1f;
        }
        else if (moveRight)
        {
            horizontalInput = 1f;
        }

        rig.velocity = new Vector2(horizontalInput * moveSpeed, rig.velocity.y);

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
        else if (horizontalInput != 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        }
        else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }
    }

    public void resetJump()
    {
        jumpsRemaining = 2;
    }

    public enum PlayerAnimation
    {
        idle, walk, jump, die, run, hurt
    }

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

    public void CompleteMinigame()
    {
        if (gameProgressController != null)
        {
            gameProgressController.CompleteMinigame();
        }

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void EndGame()
    {
        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneNames.LOSE_G);
    }

    

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
        textOverlayController.ShowPanel(card.title, card.subtitle, card.description, "Continuar", sprite, EndIfWin);
    }

    public void UpdateCards()
    {
        currentCards++;
    }

    public void EndIfWin()
    {
        if (currentCards == TOTAL_CARDS)
        {
            StartCoroutine(FinishRoutine());
            
        }
    }

    private IEnumerator FinishRoutine()
    {
        int points = 1000 + PlayerPrefs.GetInt(LIVES, 0) * 100 + 2000 / tiempojugado;
        StopCoroutine(timeCoro);
        yield return PlayerProgress.Instance.UpdateProgess(MiniGameNames.PLATAFORMS, points, tiempojugado);

        PlayerPrefs.SetString(Preferences.PREVIOUS_GAME, SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(SceneNames.WIN_G);
    }

    private IEnumerator ContarTiempo()
    {

        while (true)
        {
            tiempojugado++;
            yield return new WaitForSeconds(1);
        }
    }
}
