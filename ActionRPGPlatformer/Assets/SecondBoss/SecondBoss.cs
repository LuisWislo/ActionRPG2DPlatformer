using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour
{
    private Player ply;
    private bool spawn;
    private AudioManager audio;
    private Rigidbody2D rb;
    private bool wall;
    private int facing;
    private bool canJump;
    private bool attNoHB;
    private GameObject tempNair;
    private bool phase2;
    private bool jumpCD;

    public bool nairing, jabbing;
    public EnemyBeing self;
    public bool grounded;
    public Animator anim;
    public LayerMask groundLayer;
    public float speed;
    public float lowJump;
    public float highJump;
    public SpriteRenderer heaalth0, health1;
    public GameObject bar;
    public GameObject jab;
    public float highDist;
    public GameObject nair;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("bg_2").GetComponent<SpriteRenderer>().enabled = false;
        ply = FindObjectOfType<Player>();
        spawn = false;
        audio = FindObjectOfType<AudioManager>();
        audio.Stop("SecondLevel");
        GetComponent<SpriteRenderer>().enabled = false;
        heaalth0.enabled = false;
        health1.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        rb = GetComponent<Rigidbody2D>();
        facing = 1;
        wall = false;
        canJump = true;
        GetComponent<BoxCollider2D>().enabled = false;
        attNoHB = false;
        phase2 = false;
        jabbing = false;
        nairing = false;
        jumpCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded();
        wall = IsWalled();

        if (nairing && grounded)
        {
            nairing = false;
        }
        if (jabbing && !grounded)
        {
            jabbing = false;
        }

        if (!spawn)
        {
            if (ply.transform.position.x >= 16)
            {
                spawn = true;
                audio.Play("SecondBoss");
                GameObject[] temp = GameObject.FindGameObjectsWithTag("tile_boss");
                for (int i=0; i<temp.Length; i++)
                {
                    temp[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
                GameObject.Find("bg_1").GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("bg_2").GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<CapsuleCollider2D>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                heaalth0.enabled = true;
                health1.enabled = true;
            }
        } else
        {
            if (!phase2 && ((self.health * 1.0) / self.maxHealth) < 0.6f)
            {
                
                speed += 2;
                self.attack = Mathf.FloorToInt(self.attack * 1.2f);
                phase2 = true;
            }

            canJump = grounded && !nairing && !jabbing && !jumpCD;

            if (!jabbing)
            {
                if (facing == 1 && wall && ply.transform.position.x - transform.position.x > 0.3f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else if (facing == -1 && wall & ply.transform.position.x - transform.position.x < -0.3f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else if (ply.transform.position.x - transform.position.x > 0.3f)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else if (ply.transform.position.x - transform.position.x < -0.3f)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                /*if (ply.transform.position.x - transform.position.x > 0 && Mathf.Abs(transform.position.y - ply.transform.position.y) > 0.3f)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else if (ply.transform.position.x - transform.position.x < 0 && Mathf.Abs(transform.position.y - ply.transform.position.y) > 0.3f)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }*/
            } else
            {
                rb.velocity = Vector2.zero;
            }

            if (wall && grounded && canJump && ((ply.transform.position.x - transform.position.x > 0.3f) || (ply.transform.position.x - transform.position.x < -0.3f)))
            {
                StartCoroutine(Jump(lowJump));
            } else if (Mathf.Abs(ply.transform.position.x - transform.position.x) < 0.3f)
            {
                if (ply.transform.position.x - transform.position.x >= 0)
                {
                    facing = 1;
                } else
                {
                    facing = -1;
                }

                float yDist = ply.transform.position.y - transform.position.y;
                if (!attNoHB)
                    StartCoroutine(Att(yDist));
            }
        }

        if (rb.velocity.x > 0)
        {
            facing = 1;
        }
        else if (rb.velocity.x < 0)
        {
            facing = -1;
        }

        transform.localScale = new Vector3(facing, 1f, 1f);
        if (bar != null && spawn)
            bar.transform.localScale = new Vector3(facing*3, 2, 1);

        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("speedY", rb.velocity.y);
        anim.SetBool("grounded", grounded);
        anim.SetBool("nairing", nairing);
        anim.SetBool("jabbing", jabbing);
    }

   IEnumerator Jump(float force)
    {
        jumpCD = true;
        audio.Play("BossJump");
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        jumpCD = false;
    }

    IEnumerator Att(float dist)
    {
        attNoHB = true;
        if (dist <= 0.5f && !jabbing && !nairing && grounded && Mathf.Abs(transform.position.y - ply.transform.position.y) < 0.3f)
        {
            StartCoroutine(Jab());
        } else if (!nairing && !jabbing && grounded && canJump && (dist <= highDist || !phase2))
        {
            StartCoroutine(Jump(lowJump));
            StartCoroutine(Nair0(0.05f));
        } else if (phase2 && canJump && grounded && !nairing)
        {
            StartCoroutine(Jump(highJump));
            StartCoroutine(Nair0(0.2f));
        }
        yield return new WaitForSeconds(0.1f);
        attNoHB = false;
    }

    IEnumerator Nair0(float x)
    {
        yield return new WaitForSeconds(x);
        StartCoroutine(Nair1());
    }

    IEnumerator Nair1()
    {
        nairing = true;
        audio.Play("BossSword");
        tempNair = Instantiate(nair, transform.position + new Vector3(0.1f * facing, 0, 0), Quaternion.identity);
        tempNair.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(2);
        nairing = false;
    }

    IEnumerator Jab()
    {
        jabbing = true;
        audio.Play("BossSword");
        Instantiate(jab, transform.position + new Vector3(0.3f * facing, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.32f);
        jabbing = false;
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position + new Vector3(0, 0, 0);
        Vector2 direction = Vector2.down;
        float distance = 0.5f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    private bool IsWalled()
    {
        Vector2 position = transform.position + new Vector3(0, -0.3f, 0);
        Vector2 direction = Vector2.right * facing;
        float distance = 0.4f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
