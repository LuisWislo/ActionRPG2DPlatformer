using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class arrowScript1 : MonoBehaviour
{
    public GameObject cannon;
    private int frame = 0;
    private static GameObject clone;
    public Tilemap tilem;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (frame == 150)
        {
            clone = Instantiate(gameObject, cannon.transform.position, cannon.transform.rotation);

            clone.AddComponent<DiagonalArrowScript>();
            clone.AddComponent<Rigidbody2D>();

            clone.GetComponent<DiagonalArrowScript>().tm = tilem;

            Rigidbody2D rb1 = clone.GetComponent<Rigidbody2D>();

            rb1.velocity = -cannon.transform.position;

            Destroy(clone, 4.0f);

            frame = -1;
        }

        frame++;
    }
}
