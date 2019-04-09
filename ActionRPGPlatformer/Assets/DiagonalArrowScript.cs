using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiagonalArrowScript : MonoBehaviour
{
    public Tilemap tm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Box"))
        {
            tm.SetTile(new Vector3Int(61, 11, 0), null);
            tm.SetTile(new Vector3Int(61, 12, 0), null);
        }
    }
}
