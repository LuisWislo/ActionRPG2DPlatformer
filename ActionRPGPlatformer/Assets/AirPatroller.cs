using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatroller : MonoBehaviour
{
    private Rigidbody2D patroller;
    [SerializeField] float speed;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    public GameObject egg;

    private int direction = -1;
    public Transform healthbar;

    private void Start()
    {
        StartCoroutine(ReleaseEgg());
    }

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
                healthbar.Rotate(0f, 180f, 0f);
            direction = 1;
        }
        else if (transform.position.x - rightWall.transform.position.x > 0.05f)
        {
            if (healthbar != null)
                healthbar.Rotate(0f, 180f, 0f);
            direction = -1;
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {

        if (GameObject.ReferenceEquals(collider.gameObject, leftWall))
        {
            direction = 1;
            transform.Rotate(new Vector2(0f, 180f));
            if (healthbar != null)
                healthbar.Rotate(0f, 180f, 0f);
        }

        else if (GameObject.ReferenceEquals(collider.gameObject, rightWall))
        {

            direction = -1;
            transform.Rotate(new Vector2(0f, 180f));
            if (healthbar != null)
                healthbar.Rotate(0f, 180f, 0f);
        }

    }*/

    IEnumerator ReleaseEgg()
    {
        Instantiate(egg, transform.position-new Vector3(0f,0.25f), Quaternion.identity);
        yield return new WaitForSeconds(1);
        StartCoroutine(ReleaseEgg());
    }
}
