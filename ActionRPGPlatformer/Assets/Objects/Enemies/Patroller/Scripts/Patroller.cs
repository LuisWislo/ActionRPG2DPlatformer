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
    public float power;

    void Update()
    {
        patroller = GetComponent<Rigidbody2D>();
        patroller.velocity = new Vector2(speed*Time.deltaTime*direction, 0f);

        if (patroller.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (patroller.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Mathf.Abs(transform.position.x -  leftWall.transform.position.x) < 0.05f)
        {
            healthbar.Rotate(0f, 180f, 0f);
            direction = 1;
        } else if (Mathf.Abs(transform.position.x - rightWall.transform.position.x) < 0.05f)
        {
            healthbar.Rotate(0f, 180f, 0f);
            direction = -1;
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (GameObject.ReferenceEquals(collider.gameObject, leftWall))
        {
            transform.Rotate(new Vector2(0f, 180f));
            healthbar.Rotate(0f, 180f, 0f);
            direction = 1;
        }

        else if(GameObject.ReferenceEquals(collider.gameObject, rightWall))
        {
            transform.Rotate(new Vector2(0f, 180f));
            healthbar.Rotate(0f, 180f, 0f);
            direction = -1;
        }
        
    }*/

}