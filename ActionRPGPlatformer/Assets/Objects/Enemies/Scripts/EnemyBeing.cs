using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeing : MonoBehaviour
{
    public GameObject deathParticle; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            StartCoroutine(Die());
            
        }
    }

    IEnumerator Die()
    {
        Instantiate(deathParticle, transform);
        enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
