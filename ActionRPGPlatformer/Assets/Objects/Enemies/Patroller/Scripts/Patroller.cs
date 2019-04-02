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
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (GameObject.ReferenceEquals(collider.gameObject, leftWall))
        {
            direction = 1;
            transform.Rotate(new Vector2(0f, 180f));
            healthbar.Rotate(0f, 180f, 0f);
        }

        else if(GameObject.ReferenceEquals(collider.gameObject, rightWall))
        {

            direction = -1;
            transform.Rotate(new Vector2(0f, 180f));
            healthbar.Rotate(0f, 180f, 0f);
        }
        
    }
    
}