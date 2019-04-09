using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public GameObject collisionParticle;
    float projectileSpeed = 1.5f;
    private Transform target;
    private Vector2 displacement;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        displacement = new Vector2(target.localPosition.x - transform.localPosition.x, target.localPosition.y - transform.localPosition.y) * 2;
        float zRotation = (float)System.Math.Atan((double)displacement.y / (double)displacement.x) * (180 / Mathf.PI);
        if (zRotation < 0 && target.position.y > transform.position.y|| zRotation > 0 && target.position.y < transform.position.y) zRotation += 180;
        //else if (zRotation > 0 && target.position.y < transform.position.y) zRotation += 180;
        transform.Rotate(new Vector3(0f, 0f, zRotation));
        
        }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.RotateTowards(transform.position, target.position, 360f, 360f));
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
        Destroy(gameObject, 8);

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
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I love you!");
        Instantiate(collisionParticle, transform);
        Destroy(gameObject);
    }*/
}
