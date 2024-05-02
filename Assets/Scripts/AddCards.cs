using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private int rows = 3;
    
    [SerializeField]
    private int columns = 4;

    void Awake()
    {
        for(int i = 0; i < rows * columns; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.name = i + "";
            newCard.transform.SetParent(puzzleField, false);
            
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
