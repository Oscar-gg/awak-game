using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    private Slider slider;
    private Text tipText;
    private Image background;

    private float sliderProgess;

    void Awake()
    {
        slider = FindAnyObjectByType<Slider>();
        tipText = GameObject.Find("TipText").GetComponent<Text>();
        background = GameObject.Find("Background").GetComponent<Image>();
        sliderProgess = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        background.overrideSprite = Tips.GetRandomBackground();
        tipText.text = "Tip: " + Tips.GetRandomTip();
        sliderProgess = 0.1f;
        slider.value = sliderProgess;
        StartCoroutine(ProgressBar());
        StartCoroutine(MainCoroutine());
    }


    private IEnumerator MainCoroutine()
    {
        yield return StartCoroutine(PlayerProgress.Instance.NewLogin());
        sliderProgess = 0.5f;
        yield return new WaitForSecondsRealtime(0.3f);

        while (sliderProgess < 1)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0.3f, 0.6f));
            sliderProgess += 0.1f;
        }
        SceneManager.LoadScene(PlayerPrefs.GetString(Preferences.RETURN_SCENE_PREF, SceneNames.LOGIN));
    }

    public IEnumerator ProgressBar()
    {
        while (sliderProgess != 1)
        {
            if (slider.value < sliderProgess)
            {
                slider.value += (sliderProgess-slider.value)/10;
            }
            yield return null;
        }
    }

}
