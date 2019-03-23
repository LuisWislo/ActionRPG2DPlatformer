using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Player ply;
    public int attackNo; //0 Jab    1 Poke  2 Nair
    // Start is called before the first frame update
    void Start()
    {
        ply = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (attackNo)
        {
            case 0:
                if (!ply.jabbing)
                {
                    Destroy(gameObject);
                }
                break;
            case 1:
                if (!ply.poking)
                {
                    Destroy(gameObject);
                }
                break;
            case 2:
                if (!ply.nairing)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
