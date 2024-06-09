using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    private Text progreso;

    private void Awake()
    {
        progreso = GameObject.Find("ProgressText").GetComponent<Text>();
    }
    void Start()
    {
        Debug.Log(PlayerProgress.Instance.GetLevelsCompleted());
        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        yield return StartCoroutine(PlayerProgress.Instance.UpdateProgess(MiniGameNames.MAZE, 300, 100));
        yield return StartCoroutine(PlayerProgress.Instance.UpdateProgess(MiniGameNames.ASSOCIATION, 100, 100));
        yield return StartCoroutine(PlayerProgress.Instance.UpdateProgess(MiniGameNames.TEDI_BOSS, 50, 100));
        yield return StartCoroutine(PlayerProgress.Instance.UpdateProgess(MiniGameNames.COMMUNICATIONS_BOSS, 1, 100));

        Debug.Log("Points: " + PlayerProgress.Instance.GetPoints());
        //Debug.Log(PlayerProgress.Instance.GetProgress());
        progreso.text = PlayerProgress.Instance.GetProgress() * 100 + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
