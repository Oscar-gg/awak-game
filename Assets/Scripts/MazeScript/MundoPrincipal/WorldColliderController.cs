using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script es para poder desbloquear los niveles 


public class WorldColliderController : MonoBehaviour
{
    public int worldIndex; // �ndice del mundo que este collider representa
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
                Debug.Log("El mundo " + worldIndex + " est� bloqueado.");
                // Aqu� puedes agregar l�gica adicional, como mostrar un mensaje al jugador.
            }
        }
    }

    private void EnterWorld()
    {
        // Aqu� va la l�gica para cargar el mundo
        Debug.Log("Entrar al mundo " + worldIndex);
        // Por ejemplo, podr�as usar una escena diferente para cada mundo:
        // SceneManager.LoadScene("World" + worldIndex);
    }
}
