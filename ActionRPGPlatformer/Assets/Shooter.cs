using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Transform target;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootsEvery;
    [SerializeField] float jumpsEvery;
    private float actualShootInterval, actualJumpInterval;
    private bool facesRight = false;
    //[SerializeField] Transform groundCheck;
    //private bool isGrounded;
    //[SerializeField] LayerMask layerGround;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        actualShootInterval = shootsEvery;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGrounded);
        if (actualJumpInterval <= 0)
        {
            body.AddForce(new Vector2(0f, 250f));
            //Debug.Log("Here!");
            actualJumpInterval = jumpsEvery;
        }
        else
        {
            actualJumpInterval -= Time.deltaTime;
        }

        if (actualShootInterval <= 0)
        {
            Instantiate(projectile, shootPoint.position, transform.rotation);
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

   /* private void FixedUpdate()
    {
        /*
        isGrounded = false;
        //Check if player is grounded:
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 1f, layerGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }

        }
    }*/

}

   
