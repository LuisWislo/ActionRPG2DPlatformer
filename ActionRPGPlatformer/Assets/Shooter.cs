using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D body;
    Transform target;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootsEvery;
    [SerializeField] float jumpsEvery;
    private float actualShootInterval, actualJumpInterval;
    private bool facesRight = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        actualShootInterval = shootsEvery;
        actualJumpInterval = jumpsEvery;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualJumpInterval <= 0)
        {
            body.AddForce(new Vector2(0f, 250f));
            actualJumpInterval = jumpsEvery;
        }
        else
        {
            actualJumpInterval -= Time.deltaTime;
        }

        if (actualShootInterval <= 0)
        {
            Instantiate(projectile, shootPoint.position, Quaternion.identity);
            actualShootInterval = shootsEvery;
        }
        else
        {
            actualShootInterval -= Time.deltaTime;
        }


        if(!facesRight && target.position.x > transform.position.x)
        {
            transform.Rotate(0f, 180f,0f);
            facesRight = !facesRight;
        }

        if(facesRight && target.position.x < transform.position.x)
        {
            transform.Rotate(0f, 180f, 0f);
            facesRight = !facesRight;
        }
    }

}

   
