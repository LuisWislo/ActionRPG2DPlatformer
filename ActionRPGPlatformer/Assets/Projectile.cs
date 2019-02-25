using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileSpeed = 1f;
    private Transform target;
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
       

        targetPos = new Vector2(target.position.x,target.position.y);


    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed*Time.deltaTime);

        if (transform.position.Equals(targetPos))
        {
            Destroy(gameObject);
        }
           
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }   
    }

}
