using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg_AirPatroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject collisionParticle;

    void Start()
    {
        Destroy(gameObject, 4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            Instantiate(collisionParticle, transform);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,1);
        }
    }
}
