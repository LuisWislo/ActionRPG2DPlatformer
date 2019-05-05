 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    private Rigidbody2D patroller;
    [SerializeField] float speed;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    
    private int direction=-1;
    public Transform healthbar;


    void Update()
    {
        patroller = GetComponent<Rigidbody2D>();
        patroller.velocity = new Vector2(speed * Time.deltaTime * direction, 0f);


        if (patroller.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (patroller.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (transform.position.x - leftWall.transform.position.x < 0.05f)
        {
            if (healthbar != null)
                healthbar.localScale = new Vector3(-1, 1, 1);
            direction = 1;
        }
        else if (transform.position.x - rightWall.transform.position.x > 0.05f)
        {
            if (healthbar != null)
                healthbar.localScale = new Vector3(1, 1, 1);
            direction = -1;
        }
    }
}