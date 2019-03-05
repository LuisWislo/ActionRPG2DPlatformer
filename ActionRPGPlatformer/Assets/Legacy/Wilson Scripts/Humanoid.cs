using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Vector2 jumpSpeed;
    [SerializeField] Vector2 slideSpeed;
    [SerializeField] float runSpeed;

    float horizontalDetection;
    float verticalDetection;
    int slideDirection;


    [SerializeField] Rigidbody2D humanoid;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask layerGround;
    

    Vector3 velocity = Vector3.zero;

    bool isGrounded;
    bool isFacingRight = true;
    bool isCrouching;
    //bool isUnderWater
    //bool isOnWaterSurface

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        horizontalDetection = Input.GetAxisRaw("Horizontal");
        verticalDetection = Input.GetAxisRaw("Vertical");

        if (isFacingRight && horizontalDetection < 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector2(-1, 1);


        }

        if (!isFacingRight && horizontalDetection > 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector2(1, 1);
        }

        PollInputs();
        //Debug.Log(isFacingRight);
        //Debug.Log(horizontalDetection);
        //Debug.Log(verticalDetection);
        //Debug.Log("Is grounded:"+isGrounded);
        //Debug.Log("Is crouching:"+isCrouching);


    }

    void FixedUpdate() //Character movement and physics-related things
    {

        isGrounded = false;
        isCrouching = false;
        animator.SetBool("isAirborne", true);

        //Check if player is grounded:
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.05f, layerGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                animator.SetBool("isAirborne", false);
            }
                
        }


        Vector3 targetVelocity = new Vector2(horizontalDetection * Time.deltaTime * runSpeed, humanoid.velocity.y);
        Run(targetVelocity);
    }

    void PollInputs()
    {

        if (verticalDetection == -1f && isGrounded)
        {
            Crouch();
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            Jump();
            isGrounded = false;
        }

        if(Input.GetButtonDown("Jump") && isCrouching)
        {
            Slide();
        }
        
    }

    void Walk()
    {

    }

    void Run(Vector3 targetVelocity)
    {
        humanoid.velocity = Vector3.SmoothDamp(humanoid.velocity, targetVelocity, ref velocity, 0.025f);
        animator.SetFloat("Speed", Mathf.Abs(horizontalDetection));
    }

    void Jump()
    {
        humanoid.AddForce(jumpSpeed);
        

    }

    void Crouch()
    {
        isCrouching = true;

    }

    void Slide()
    {
        int direction = isFacingRight ? 1 : -1;
        humanoid.AddForce(slideSpeed*direction);
        
    }

    void Transform()
    {
        
    }
}
