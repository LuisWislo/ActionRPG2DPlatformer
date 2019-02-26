using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool grounded;
    private bool grndR;
    private bool grndL;
    private bool walled;
    private bool dashed;
    private bool jabbing;
    private float xLeft;
    private float xRight;
    private int facingVec;
    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    private CameraController camera;

    public float jumpForce;
    public float runSpeed;
    public float dashForce;
    public float dashDuration;
    public GameObject jab;
    public GameObject deathParticle;
    public GameObject dashParticle;
    public LayerMask groundLayer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        grounded = false;
        walled = false;
        dashed = false;
        jabbing = false;
        xLeft = -0.085f;
        xRight = 0.125f;
        facingVec = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -8)
        {
            StartCoroutine(Die());
        }
        grndL = IsGroundedLeft();
        grndR = IsGroundedRight();
        walled = IsWalled();

        if(grndL || grndR)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (!grndL && grndR && walled)
        {
            grounded = false;
        }

        if (grounded)
        {
            dashed = false;
        }
        if ((Mathf.Abs(rb.velocity.x) < runSpeed) && !jabbing)
        {
            rb.velocity = new Vector2(rb.velocity.x + 0.5f * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jabbing)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(0, jumpForce);
            } else if (walled && grndR && !grndL)
            {
                rb.velocity = new Vector2(facingVec * -3.5f, jumpForce);
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && !dashed && !grounded && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !jabbing)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.C) && !jabbing && grounded)
        {
            StartCoroutine(Jab());
        }

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            xLeft = -0.08f;
            xRight = 0.125f;
            facingVec = 1;
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            xLeft =  0.085f;
            xRight = -0.13f;
            facingVec = -1;
        }

        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("speedY", rb.velocity.y);
        anim.SetBool("grounded", grounded);
        anim.SetBool("jabbing", jabbing);
    }

    IEnumerator Dash()
    {
        Instantiate(dashParticle, transform);
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * dashForce, Input.GetAxisRaw("Vertical") * dashForce), ForceMode2D.Impulse);
        dashed = true;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = 3;
        if (!grounded)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 3, Input.GetAxisRaw("Vertical") * 3);
        }
        else
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 6, Input.GetAxis("Vertical") * 6);
        }
    }

    IEnumerator Jab()
    {
        rb.velocity = Vector2.zero;
        jabbing = true;
        Instantiate(jab, transform.position + new Vector3(0.25f * facingVec, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        jabbing = false;
    }

    IEnumerator Die()
    {
        Instantiate(deathParticle, transform);
        enabled = false;
        GetComponent<Renderer>().enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        enabled = true;
        GetComponent<Renderer>().enabled = true;
        Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Projectile")
        {
            StartCoroutine(Die());
        }
    }

    void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = respawnPoint;
        camera.SetRespawnLoc();
    }

    bool IsGroundedLeft()
    {
        Vector2 position = transform.position + new Vector3(xLeft, 0, 0);
        Vector2 direction = Vector2.down;
        float distance = 0.42f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    bool IsGroundedRight()
    {
        Vector2 position = transform.position + new Vector3(xRight, 0, 0);
        Vector2 direction = Vector2.down;
        float distance = 0.42f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    bool IsWalled()
    {
        Vector2 position = transform.position;
        Vector2 direction = transform.right * facingVec;
        float distance = 0.25f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
