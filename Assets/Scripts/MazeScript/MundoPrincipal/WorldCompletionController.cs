using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCompletionController : MonoBehaviour
{
    private GameProgressController progressController;

    private void Start()
    {
        progressController = FindObjectOfType<GameProgressController>();
    }

    // Este método debe ser llamado cuando un mundo se completa
    public void CompleteWorld(int worldIndex)
    {
        int nextWorldIndex = worldIndex + 1;
        if (nextWorldIndex < progressController.worldsUnlocked.Length)
        {
            progressController.UnlockWorld(nextWorldIndex);
        }
    }

    public void Ignore()
    {

    }
}

