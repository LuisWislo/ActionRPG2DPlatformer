using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public EnemyBeing self;
    public Animator anim;
    public int range;
    public float speed;
    public Transform healthbar;
    public GameObject hitbox;
    private Rigidbody2D rb;
    private float xPos;
    private float speedPriv;
    private Player ply;
    private bool att;
    private CapsuleCollider2D coll;
    private GameObject temp;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ply = FindObjectOfType<Player>();
        xPos = transform.position.x;
        speedPriv = -speed;
        att = false;
        coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!self.dead)
        {
            if (transform.position.x < xPos - range)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                healthbar.localScale = new Vector3(-1, 1, 1);
                speedPriv = speed;
            }
            else if (transform.position.x > xPos + range)
            {
                transform.localScale = new Vector3(1, 1, 1);
                healthbar.localScale = new Vector3(1, 1, 1);
                speedPriv = -speed;
            }

            if (ply.transform.position.x < transform.position.x && speedPriv > 0 && !att)
            {
                self.canGetGurt = true;
            }
            else if (ply.transform.position.x > transform.position.x && speedPriv < 0 && !att)
            {
                self.canGetGurt = true;
            }
            else
            {
                if (!att)
                    self.canGetGurt = false;
                if (Mathf.Abs(ply.transform.position.x - transform.position.x) < 1 && !att)
                {
                    coll.enabled = false;
                    att = true;
                    self.canGetGurt = true;
                    StartCoroutine(Attack());
                }
            }

            if (!att)
            {
                rb.velocity = new Vector2(speedPriv * Time.deltaTime, 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            anim.SetBool("att", att);
        } else
        {
            coll.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            if (temp != null)
            {
                Destroy(temp.gameObject);
            }
        }
    }

    IEnumerator Attack()
    {
        temp = Instantiate(hitbox, transform.position + new Vector3(speedPriv / 200, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.8f);
        Destroy(temp.gameObject);
        att = false;
    }
}
