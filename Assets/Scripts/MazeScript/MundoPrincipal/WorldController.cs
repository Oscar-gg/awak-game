using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    private GameProgressController progressController;
    public List<GameObject> locks; // Lista de candados
    public List<GameObject> BlockCollider;



    // Start is called before the first frame update
    void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();

        // Actualizar el estado inicial del mundo
        UpdateWorld();
        CollidersWorld();


    }




    public void UpdateWorld()
    {
        // Desactivar candados 
        for (int i = 0; i < locks.Count; i++)
        {
            if (progressController.currentProgress >= (i + 1) * 2)
            {
                locks[i].SetActive(false);
            }
        }
    }


    public void CollidersWorld()
    {
        for (int i = 0; i<BlockCollider.Count; i++)
        {
            if (progressController.currentProgress >= (i + 1) * 2)
            {
                BlockCollider[i].SetActive(false);
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        UpdateWorld();
        CollidersWorld();
    }
}
