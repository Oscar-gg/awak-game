using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Camera m_Camera;
    GameObject player;

    private float cameraInitialPosY;
    private float playerInitialPosY;

    private int currCameraPosY = 0;

    private static readonly float MOVEMENT_UNIT = 1.5f;
    private static readonly float MOVEMENT_DELAY = 0.05f;
    private static readonly float MOVEMENT_BASE_SPEED = 0.05f;
    private static readonly float DISTANCE_ADDED_SPEED = 1.5f;

    private static float MAX_HEIGHT = 9.0f;
    private static int MAX_POS = (int) (MAX_HEIGHT / MOVEMENT_UNIT);

    private Coroutine cameraMovement;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        player = GameObject.Find("AnaPlatforms");
        cameraInitialPosY = m_Camera.transform.position.y;

        playerInitialPosY = player.transform.position.y;
    }

    IEnumerator MoveCameraY(int finalPose)
    {
        float targetPose = cameraInitialPosY + MOVEMENT_UNIT * finalPose;

        while (m_Camera.transform.position.y > targetPose)
        {

            Vector3 newPos = new Vector3(m_Camera.transform.position.x, m_Camera.transform.position.y - MOVEMENT_BASE_SPEED, m_Camera.transform.position.z);
            m_Camera.transform.position = newPos;

            float delta = Mathf.Abs(targetPose - m_Camera.transform.position.y) * DISTANCE_ADDED_SPEED;
            yield return new WaitForSeconds(MOVEMENT_DELAY / (1 + delta));
        }

        while (m_Camera.transform.position.y < targetPose)
        {
            Vector3 newPos = new Vector3(m_Camera.transform.position.x, m_Camera.transform.position.y + MOVEMENT_BASE_SPEED, m_Camera.transform.position.z);
            m_Camera.transform.position = newPos;
            
            float delta = Mathf.Abs(targetPose - m_Camera.transform.position.y) * DISTANCE_ADDED_SPEED;
            yield return new WaitForSeconds(MOVEMENT_DELAY / (1 + delta));
        }
        cameraMovement = null;
    }


    // Update is called once per frame
    void Update()
    {
        float heightDiff = player.transform.position.y - playerInitialPosY;

        int nextPoseY = (int)(heightDiff / MOVEMENT_UNIT);
        nextPoseY = Mathf.Min(nextPoseY, MAX_POS);

   

        if (nextPoseY != currCameraPosY)
        {
            if (cameraMovement != null)
            {
                StopCoroutine(cameraMovement);
            }
            cameraMovement = StartCoroutine(MoveCameraY(nextPoseY));
            currCameraPosY = nextPoseY;

        }

    }
}
