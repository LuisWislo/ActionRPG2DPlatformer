using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class arrowScript : MonoBehaviour
{
    public GameObject cannon1, cannon2, cannon3;
    private int frame = 0;
    private static GameObject clone1, clone2, clone3;
    public Tilemap tilem;
    public bool shoot;
    private AudioManager audio;

    void Start()
    {
        if (shoot)
        {
            audio = FindObjectOfType<AudioManager>();
            audio.Play("SettingsSong");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            if (frame == 150)
            {
                clone1 = Instantiate(gameObject, cannon1.transform.position, cannon1.transform.rotation);
                clone2 = Instantiate(gameObject, cannon2.transform.position, cannon2.transform.rotation);
                clone3 = Instantiate(gameObject, cannon3.transform.position, cannon3.transform.rotation);

                clone3.AddComponent<DiagonalArrowScript>();
                clone3.GetComponent<DiagonalArrowScript>().tm = tilem;

                clone1.GetComponent<arrowScript>().shoot = false;
                clone2.GetComponent<arrowScript>().shoot = false;
                clone3.GetComponent<arrowScript>().shoot = false;

                clone1.AddComponent<Rigidbody2D>();
                clone2.AddComponent<Rigidbody2D>();
                clone3.AddComponent<Rigidbody2D>();

                Rigidbody2D rb1 = clone1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = clone2.GetComponent<Rigidbody2D>();
                Rigidbody2D rb3 = clone3.GetComponent<Rigidbody2D>();

                rb1.velocity = Vector2.down;
                rb2.velocity = Vector2.down;
                rb3.velocity = -cannon3.transform.position;

                Destroy(clone1, 4.0f);
                Destroy(clone2, 4.0f);
                Destroy(clone3, 4.5f);

                frame = -1;
            }

            frame++;
        }
    }
}
