using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Rotate : MonoBehaviour
{


    private Coroutine rotatingCoro;

    IEnumerator DoRotation(float speed, float amount, Vector3 axis)
    {
        Vector3 initialPos = transform.position;
        Quaternion initialRotation = transform.rotation;

        float rot = 0f;
        while (rot < amount)
        {
            yield return null;
            //float delta = Mathf.Min(speed * Time.deltaTime, amount - rot);
            float delta = Mathf.Max((amount - rot) * speed, 0.025f);
            transform.RotateAround(transform.position, axis, delta);
            rot += delta;
        }

        initialRotation.eulerAngles.Set(initialRotation.eulerAngles.x,
                                       initialRotation.eulerAngles.y + amount,
                                       initialRotation.eulerAngles.z);

        yield return new WaitForSeconds(0.5f);

        //transform.SetPositionAndRotation(initialPos, initialRotation);

        StopRotating();
    }

    void StopRotating()
    {
        if (rotatingCoro != null)
        {
            StopCoroutine(rotatingCoro);
            rotatingCoro = null;
        }
    }

    public void Log()
    {
        Debug.Log("Xd pa");
    }

    public IEnumerator FlipCard(Sprite newImage)
    {
        Vector3 rotateDir = new Vector3(0, 1, 0);
        rotatingCoro = StartCoroutine(DoRotation(0.05f, 90f, rotateDir));
        yield return new WaitForSeconds(0.5f);
        
        gameObject.GetComponent<Button>().image.sprite = newImage;

        rotatingCoro = StartCoroutine(DoRotation(0.05f, 90f, rotateDir));
    }
}
