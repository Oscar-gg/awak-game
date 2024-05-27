using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIControllerPlatforms : MonoBehaviour
{
    public Sprite spendLives;
    public Image[] livesImage;

    int lives = 3;

    public void UpdateLives()
    {
        lives = PlayerPrefs.GetInt(PlatformController.LIVES, 3);

        if (lives > 0 && lives < livesImage.Length)
        {   
            livesImage[lives].sprite = spendLives;
        }
    }

}
