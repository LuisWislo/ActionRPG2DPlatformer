using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    public GameObject chestClosed, chestOpened;


    // Start is called before the first frame update
    void Start()
    {
        chestOpened.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(chestClosed);

            chestOpened.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
