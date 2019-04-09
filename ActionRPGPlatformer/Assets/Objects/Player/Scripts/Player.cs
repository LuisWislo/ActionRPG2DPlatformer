using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{

    //GUI
    public TextMeshProUGUI expLevelUI;
    public TextMeshProUGUI expLevelDet;
    public TextMeshProUGUI attackUI;
    public TextMeshProUGUI defenseUI;


    //For healthbar purposes
    public Transform healthbar;
    private float maxScale;
    private float barConstant;
    

    //For experiencebar purposes
    public Transform expBar;
    private float maxScaleE;
    private float barConstantE;



    private bool grounded;
    private bool crouching;
    private bool grndR;
    private bool grndL;
    private bool walled;
    private bool dashed;
    private bool dashing;
    private float xLeft;
    private float xRight;
    private int facingVec;
    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    private CameraController camera;

    public bool jabbing;
    public bool poking;
    public bool nairing;
    public bool shooting;
    public bool shootingAir;

    public float jumpForce;
    public float runSpeed;
    public float dashForce;
    public float dashDuration;
    public GameObject jab;
    public GameObject poke;
    public GameObject nair;
    public GameObject deathParticle;
    public GameObject dashParticle;
    public LayerMask groundLayer;
    public Animator anim;
    public GameObject bullet;
    public GameObject bulletGrav;

    CapsuleCollider2D capsule;
    BoxCollider2D box;
    private Vector2 defaultCapsule = new Vector2(0.2f, 0.74f);
    private Vector2 defaultBox = new Vector2(0.21f, 0.75f);
    private Vector2 defaultOSCapsule = new Vector2(0.02f, 0f);
    private Vector2 defaultOSBox = new Vector2(0.01f, -0.01f);

    private float vertical;

    // RPG Stuff
    public int health, attack, defense, lvl, currExp, maxExp;
    private bool invin;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(expBar.localScale);
        //Debug.Log(currExp + "/" +maxExp);
        expLevelUI.text = lvl.ToString();
        expLevelDet.text = currExp + "/" + maxExp;
        attackUI.text = "A:"+attack.ToString();
        defenseUI.text = "D:"+defense.ToString();
        camera = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        grounded = false;
        walled = false;
        dashed = false;
        jabbing = false;
        poking = false;
        nairing = false;
        dashing = false;
        shooting = false;
        shootingAir = false;
        xLeft = -0.085f;
        xRight = 0.125f;
        facingVec = 1;
        capsule = gameObject.GetComponent<CapsuleCollider2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        invin = false;

        //Setting up healthbar
        maxHealth = health;
        maxScale = healthbar.localScale.x;
        barConstant = maxScale / maxHealth;

        //Setting up expbar
        maxScaleE = 1;
        barConstantE = maxScaleE / maxExp;

    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------
        crouching = false;
        vertical = Input.GetAxisRaw("Vertical");
        capsule.size = defaultCapsule;
        box.size = defaultBox;
        capsule.offset = defaultOSCapsule;
        box.offset = defaultOSBox;
        
        if(rb.velocity.x > runSpeed && !dashing)
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        } else if (rb.velocity.x < -runSpeed && !dashing)
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
        }

        if (transform.position.y < -20)
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
        //----------------------------------------------------------------

        // Walk
        if (!jabbing && !crouching)
        {
            rb.velocity = new Vector2(rb.velocity.x + 0.5f * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        }
        // Jump
        if (Input.GetButtonDown("Jump") && !jabbing && !shooting)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(0, jumpForce);
            }
            else if (walled && grndR && !grndL)
            {
                rb.velocity = new Vector2(facingVec * -4.5f, jumpForce);
            }
        }
        // Dash
        if (Input.GetButtonDown("Fire2") && !dashed && !grounded && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !jabbing)
        {
            StartCoroutine(Dash());
        }
        // Crouch
        if (vertical < -0.5 && grounded && !jabbing)
        {
            crouching = true;
            Crouch();
        }


        // Sword
        if (grounded && nairing)
        {
            StopCoroutine("Nair");
            nairing = false;
        }
        if (Input.GetButtonDown("Fire1") && !jabbing && grounded && !crouching)
        {
            StartCoroutine(Jab());
        }
        if (Input.GetButtonDown("Fire1") && crouching)
        {
            StartCoroutine(Poke());
        }
        if (Input.GetButtonDown("Fire1") && !grounded)
        {
            StartCoroutine(Nair());
        }


        // Shooting
        if (grounded && shootingAir)
        {
            StopCoroutine("Shoot");
            shooting = false;
            shootingAir = false;
        }
        if (Input.GetButtonDown("Fire3") && !shooting && !jabbing && !dashing && !poking && !nairing)
        {
            if (grounded && rb.velocity.x == 0 && !crouching)
            {
                StartCoroutine(Shoot(bullet, 0.3f, transform.position + new Vector3(0.3f *facingVec, 0.1f, 0), false));
            } else if (grounded && rb.velocity.x != 0 && !crouching)
            {
                StartCoroutine(Shoot(bullet, 0.3f, transform.position + new Vector3(0.3f * facingVec, 0.07f, 0), false));
            } else if (crouching)
            {
                StartCoroutine(Shoot(bullet, 0.2f, transform.position + new Vector3(0.4f * facingVec, -0.15f, 0), false));
            } else if (!grounded)
            {
                shootingAir = true;
                StartCoroutine(Shoot(bullet, 0.3f, transform.position + new Vector3(0.2f * facingVec, -0.21f, 0), true));
            }
        }


        if (Input.GetButtonDown("Fire4") && !shooting && !jabbing && !dashing && !poking && !nairing)
        {
            if (grounded && rb.velocity.x == 0 && !crouching)
            {
                StartCoroutine(Shoot(bulletGrav, 0.2f, transform.position + new Vector3(0.3f * facingVec, 0.1f, 0), false));
            }
            else if (grounded && rb.velocity.x != 0 && !crouching)
            {
                StartCoroutine(Shoot(bulletGrav, 0.2f, transform.position + new Vector3(0.3f * facingVec, 0.07f, 0), false));
            }
            else if (crouching)
            {
                StartCoroutine(Shoot(bulletGrav, 0.1f, transform.position + new Vector3(0.4f * facingVec, -0.15f, 0), false));
            }
            else if (!grounded)
            {
                shootingAir = true;
                StartCoroutine(Shoot(bulletGrav, 0.2f, transform.position + new Vector3(0.2f * facingVec, -0.21f, 0), true));
            }
        }


        //------------------------------------------------------------
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
        anim.SetBool("crouching", crouching);
        anim.SetBool("poking", poking);
        anim.SetBool("nairing", nairing);
        anim.SetBool("shooting", shooting);
    }

    public void UpdateExpBar(int expGiven, bool hasLeveledUp)
    {
        if (!hasLeveledUp)
        {
            expLevelDet.text = currExp + "/" + maxExp;
            expBar.localScale += new Vector3(expGiven * barConstantE, 0f, 0f);
        }
        else
        {
            expLevelUI.text = lvl.ToString();
            attackUI.text = "A:" + attack.ToString();
            defenseUI.text = "D:" + defense.ToString();
            expLevelDet.text = currExp + "/" + maxExp;
            //Debug.Log(currExp + "/" + maxExp);
            barConstantE = maxScaleE / maxExp;
            //Debug.Log(barConstantE);
            expBar.localScale = new Vector3(currExp*barConstantE, 1f, 1f);
        }
       
    }
    

    IEnumerator Nair()
    {
        nairing = true;
        GameObject nairTemp = Instantiate(nair, transform.position + new Vector3(0.1f * facingVec, 0, 0), Quaternion.identity);
        nairTemp.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.4f);
        nairing = false;
    }

    IEnumerator Poke()
    {
        poking = true;
        Instantiate(poke, transform.position + new Vector3(0.5f * facingVec, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        poking = false;
    }

    IEnumerator Dash()
    {
        dashing = true;
        Instantiate(dashParticle, transform);
        rb.velocity = Vector2.zero;
        Vector2 vecNorm = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))).normalized;
        rb.AddForce(vecNorm * dashForce, ForceMode2D.Impulse);
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
        dashing = false;
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

    IEnumerator SetInvin(float dur)
    {
        invin = true;
        for(int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(dur/5);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(dur / 5);
        }
        invin = false;
    }

    IEnumerator Shoot(GameObject obj, float t, Vector2 origin, bool diagonal)
    {
        shooting = true;
        GameObject temp = Instantiate(obj, origin, Quaternion.identity);
        if (diagonal)
        {
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * facingVec, -5), ForceMode2D.Impulse);
        } else
        {
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(10 * facingVec, 0), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(t);
        shooting = false;
        shootingAir = false;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBeing temp = collision.GetComponent<EnemyBeing>();

        if ((collision.tag == "Enemy" || collision.tag == "Projectile") && (!invin) && temp.canGetGurt)
        {
            int damage = temp.attack - defense;
            if (damage <= 0)
            {
                damage = 1;
            }
            health = health - damage;
            healthbar.localScale -= new Vector3(damage * barConstant, 0f, 0f);
            if (health <= 0)
            {
                StartCoroutine(Die());
            } else
            {
                StartCoroutine(SetInvin(0.5f));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.tag == "arrow") && (!invin))
        {
            int damage = 10;
            health = health - damage;
            healthbar.localScale -= new Vector3(damage * barConstant, 0f, 0f);
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(SetInvin(0.5f));
            }
            Destroy(collision.collider.gameObject);
        }
    }

    void Respawn()
    {
        StopCoroutine("SetInvin");
        invin = false;
        health = maxHealth;
        //healthbar
        healthbar.localScale = new Vector3(maxScale,1f,1f);
        Debug.Log(healthbar.localScale);
        currExp = (int)Mathf.Floor(currExp * 0.8f);
        
        if (currExp < 0)
        {
            currExp = 0;

        }
        UpdateExpBar(currExp, true);
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

    void Crouch()
    {
        capsule.size = new Vector2(0.4f,0.55f);
        capsule.offset = new Vector2(-0.02f, 0.038f);
        box.size = new Vector2(0.41f, 0.56f);
        box.offset = new Vector2(-0.02f,0.038f);
        //rb.velocity = Vector2.zero;

    }
}
