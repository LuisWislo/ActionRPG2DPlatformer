using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform wholebar;
    [SerializeField] Transform shootPoint;
    private Rigidbody2D body;
    Transform target;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootsEvery;
    [SerializeField] float jumpsEvery;
    private bool facesRight = false;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        StartCoroutine(Jump());
    }
    
    IEnumerator Shoot()
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(shootsEvery);
        StartCoroutine(Shoot());
    }

    IEnumerator Jump()
    {
        Debug.Log("Jump frog!");
        body.gravityScale = 1f;
        body.AddForce(new Vector2(0f, 400f));
        yield return new WaitForSeconds(jumpsEvery);
        StartCoroutine(Jump());
    }

    void Update()
    {
        
        if(!facesRight && target.position.x > transform.position.x)
        {
            transform.Rotate(0f, 180f,0f);
            if(wholebar!=null)
                wholebar.Rotate(0f, 180f, 0f);
            facesRight = !facesRight;
        }

        if(facesRight && target.position.x < transform.position.x)
        {
            transform.Rotate(0f, 180f, 0f);
            if (wholebar != null)
                wholebar.Rotate(0f, 180f, 0f);
            facesRight = !facesRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            body.velocity = Vector3.zero;
            body.gravityScale = 0f;
        }
    }

}

   
