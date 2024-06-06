using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartCredits : MonoBehaviour
{
    public AudioClip musicClip;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CreditIndex", 0);
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Begin()
    {
        if (musicClip == null)
        {
            Debug.LogError("No music clip assigned!");
            yield break;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject!");
            yield break;
        }

        audioSource.clip = musicClip;  // Assign the audio clip
        audioSource.loop = true;       // Set looping to play continuously
        audioSource.playOnAwake = false; // Don't play on object creation (optional)
        audioSource.volume = 1.0f;      // Set volume (0.0f to 1.0f)

        audioSource.Play();  // Start playing the music
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("CreditsScene");
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StopMusic()
    {
        Destroy(gameObject);
    }
}
