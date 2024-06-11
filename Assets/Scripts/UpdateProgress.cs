using UnityEngine;
using UnityEngine.UI;

public class UpdateProgress : MonoBehaviour
{

    private Text text;
    private Slider progressBar;
    // Start is called before the first frame update
    void Awake()
    {
        text = GameObject.Find("ProgressText").GetComponent<Text>();
        progressBar = GameObject.Find("SliderPuntaje").GetComponent<Slider>();
    }

    void Start()
    {
        float progress = PlayerProgress.Instance.GetProgress();
        text.text = progress * 100 + "%";
        progressBar.value = progress;
    }
}
