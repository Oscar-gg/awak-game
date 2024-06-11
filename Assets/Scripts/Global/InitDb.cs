using UnityEngine;

public class Testing : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Niveles completados: " + PlayerProgress.Instance.GetLevelsCompleted());
    }

}
