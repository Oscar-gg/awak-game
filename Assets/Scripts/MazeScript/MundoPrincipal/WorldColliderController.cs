using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script es para poder desbloquear los niveles 


public class WorldColliderController : MonoBehaviour
{
    public int worldIndex; // Índice del mundo que este collider representa
    public GameObject lockedImage; // Objeto de UI que muestra el candado cerrado
    private GameProgressController progressController;

    private void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();

        // Actualizar el estado inicial del mundo
        UpdateWorldState();
    }

    public void UpdateWorldState()
    {
        if (progressController.IsWorldUnlocked(worldIndex))
        {
            lockedImage.SetActive(false);
        }
        else
        {
            lockedImage.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (progressController.IsWorldUnlocked(worldIndex))
            {
                EnterWorld();
            }
            else
            {
                Debug.Log("El mundo " + worldIndex + " está bloqueado.");
                
            }
        }
    }

    private void EnterWorld()
    {
        
        Debug.Log("Entrar al mundo " + worldIndex);
        
        // SceneManager.LoadScene("World" + worldIndex);
    }
}
