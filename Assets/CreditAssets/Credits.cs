using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Text title;
    public Text content;
    public Animator animator;
    public Animation an;
    public Image bgtop;
    public Image bgbottom;
    public Sprite[] spritestop;
    public Sprite[] spritesbottom;

    

    private int index = 0;

    List<string[]> credits = new List<string[]>()
    {
        new string[] { "Development and Programming", "banana" },  // First array with strings "apple" and "banana"
        new string[] { "Design", "gradsspe" },
        new string[] { "Graphics and Art", "grape" },   // Second array with strings "orange" and "grape"
        new string[] { "Audio", "gradsspe" },
        new string[] { "Special Thanks", "gradsspe" },
        new string[] { "Developed in Unity", "" }
    };

    void Start()
    {
        StartCoroutine(Load());

    }


    IEnumerator Load()
    {
        index = PlayerPrefs.GetInt("CreditIndex");
        Debug.Log(index);
        if (index < credits.Count)
        {
            title.text = credits[index][0];
            content.text = credits[index][1];

            if (spritestop.Length >= index)
            { 
                bgtop.sprite = spritestop[index];
                bgbottom.sprite = spritesbottom[index];
            }

            yield return new WaitForSeconds(8);
            PlayerPrefs.SetInt("CreditIndex", index + 1);
            SceneManager.LoadScene("CreditsScene");
        } else
        {
            PlayerPrefs.SetInt("CreditIndex", 0);
            SceneManager.LoadScene("EndScene");
        }

        
    }

    IEnumerator ChangeTextCoroutine()
    {
        Debug.Log("init cor");
        while (index < credits.Count)
        {
            // Get text from dictionary based on index
            string textToDisplay = credits[index][0];

            // Update Text objects
            title.text = credits[index][0];
            content.text = credits[index][1];

            if (spritestop.Length >= index)
            {
                bgtop.sprite = spritestop[index];
                bgbottom.sprite = spritesbottom[index];
            }

            // Play the animation to show the texts again
            //animator.enabled = false;
            //animator.enabled = true;
            //animator.SetTrigger("show");
            //animator.Rewind("Credits");
            animator.Play("Credits");
            //animator.Play("Credits");
            //an.Play();

            // Wait for the animation and specified time
            yield return new WaitForSeconds(8); // Add animation length

            index++;
            Debug.Log(index);
        }

        // Reset index for looping through the dictionary again (optional)
        index = 0;
    }
}
