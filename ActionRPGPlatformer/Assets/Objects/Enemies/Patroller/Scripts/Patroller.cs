using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] Rigidbody2D patroller;
    [SerializeField] float speed;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    
    private int direction=-1;
    public Transform healthbar;
    public float power;
   

    
    void Update()
    {
        patroller.velocity = new Vector2(speed*Time.deltaTime*direction, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (GameObject.ReferenceEquals(collider.gameObject, leftWall))
        {
            direction = 1;
            
            
        }else if(GameObject.ReferenceEquals(collider.gameObject, rightWall))
        {

            direction = -1;
        }
        transform.Rotate(new Vector2(0f, 180f));
        healthbar.Rotate(0f, 180f, 0f);
    }

    public IEnumerator Hit()
    {
        GetComponent<EnemyBeing>().canHurt = false;
        //Debug.Log("Enemy hit");
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }

        GetComponent<EnemyBeing>().canHurt = true;



    }
}