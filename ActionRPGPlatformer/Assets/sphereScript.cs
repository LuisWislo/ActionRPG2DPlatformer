using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class sphereButtonScript : MonoBehaviour
{
    public Tilemap tm;
    public GameObject[] tilePositions;
    private TileBase floor;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        floor = tm.GetTile(new Vector3Int(23, 6, 0));

        foreach(GameObject tp in tilePositions)
        {
            tp.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("bullet"))
        {
            if (gameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                switch (gameObject.name)
                {
                    case "1":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;
                        Vector3Int[] vs = new Vector3Int[tilePositions.Length];

                        for (int i = 0; i < tilePositions.Length; i++)
                        {
                            vs[i] = new Vector3Int((int)tilePositions[i].transform.position.x, (int)tilePositions[i].transform.position.y, (int)tilePositions[i].transform.position.z);

                            tm.SetTile(vs[i], floor);
                            
                        }

                        StartCoroutine(wait(10.0f, vs));

                        break;

                    case "2":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;
                        Vector3Int[] vc = new Vector3Int[tilePositions.Length];

                        for (int i = 0; i < tilePositions.Length; i++)
                        {
                            vc[i] = new Vector3Int((int)tilePositions[i].transform.position.x, (int)tilePositions[i].transform.position.y, (int)tilePositions[i].transform.position.z);

                            tm.SetTile(vc[i], floor);
                        }

                        StartCoroutine(wait(7.0f, vc));

                        break;

                    case "3":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;
                        Vector3Int[] vt = new Vector3Int[tilePositions.Length];

                        for (int i = 0; i < tilePositions.Length; i++)
                        {
                            vt[i] = new Vector3Int((int)tilePositions[i].transform.position.x, (int)tilePositions[i].transform.position.y, (int)tilePositions[i].transform.position.z);

                            tm.SetTile(vt[i], floor);
                        }

                        StartCoroutine(wait(5.0f, vt));

                        break;

                    case "4":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;
                        Vector3Int[] ve = new Vector3Int[tilePositions.Length];

                        for (int i = 0; i < tilePositions.Length; i++)
                        {
                            ve[i] = new Vector3Int((int)tilePositions[i].transform.position.x, (int)tilePositions[i].transform.position.y, (int)tilePositions[i].transform.position.z);

                            tm.SetTile(ve[i], floor);
                        }

                        StartCoroutine(wait(4.0f, ve));

                        break;

                    case "5":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;


                        break;

                    case "6":
                        gameObject.GetComponent<Renderer>().material.color = Color.green;


                        break;

                    default: break;
                }
            }
        }
    }

    IEnumerator wait(float x, Vector3Int[] vctr)
    {
        yield return new WaitForSeconds(x);

        foreach (Vector3Int v in vctr)
        {
            tm.SetTile(v, null);
        }

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}


// gameObject.GetComponent<Renderer>().material.color = Color.green;
