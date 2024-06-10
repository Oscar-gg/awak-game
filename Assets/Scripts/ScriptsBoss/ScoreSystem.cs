using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public static int scoreValue = 0;
    Text puntuacion;
    // Start is called before the first frame update
    void Start()
    {
        puntuacion = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        puntuacion.text = "Puntaje: " + scoreValue;
    }

    public int ObtainScore()
    {
        return scoreValue;
    }
}
