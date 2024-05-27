using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollision : MonoBehaviour
{

    Sprite sp;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformController.Instance.ShowCard(sp.name, sp);
            FindObjectOfType<BossAudioManager>().PlaySound("Collect");
            GameObject.Destroy(this.gameObject);
        }
    }

}
