using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour {

    Vector2 jumpingSpeed = new Vector2(0f, 2250f);
    Vector2 slidingSpeed = new Vector2(3500f, 0f);
    private float runningSpeedF = 600f;
    private float GroundBox_Radius = 0.7f;
    //private float CeilingBox_Radius = 0.2f;
    float horizontalDetection = 0f;
    int slide_Direction;


    public Rigidbody2D player;
    public BoxCollider2D playerHead;
    public Transform GroundBox_Check;
    public Transform CeilingBox_Check;
    public LayerMask typeGround;


    bool jump_disabled = false;
    bool isGrounded=false;
    bool isJumping = false;
    bool isCrouching = false;
    bool isFacingRight = true;

    private Vector3 velocity = Vector3.zero;
    
	void Update() //Get input from player ;  Updated once per frame
    {
        
            horizontalDetection = Input.GetAxisRaw("Horizontal"); //default = 0 ; right > 1 ; left > -1
        
        //Debug.Log(horizontalDetection);
        if (!jump_disabled)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }
        }

        if (isFacingRight)
        {
            slide_Direction = 1;
        } else if (!isFacingRight)
        {
            slide_Direction = -1;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            //Debug.Log("Crouching");
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch")) 
        {
            //Debug.Log("Walking");
            isCrouching = false;
            playerHead.enabled = true;
            jump_disabled = false;
        }

        if (isFacingRight)
        {
            //Debug.Log("Facing Right");
        }
        else if (!isFacingRight)
        {
            //Debug.Log("Facing Left");
        }



    }
	
	void FixedUpdate () //Character movement
    {
        /////// Calls basic humanoid movement functions
        HumanoidMove();
        isJumping = false;
        isGrounded = false;



        ////// Checks if player is grounded

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundBox_Check.position, GroundBox_Radius,typeGround);
        for(int i=0; i<colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }
        
    }


    void HumanoidMove()
    {
        Vector3 targetVelocity = new Vector2(horizontalDetection * Time.deltaTime * runningSpeedF, player.velocity.y);
        player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref velocity, 0.05f);

        //player.velocity = horizontalDetection * Time.deltaTime * runningSpeed;

        if (isJumping && isGrounded)
        {
            isGrounded = false;
            player.AddForce(jumpingSpeed);

        }

        if (isGrounded)
        {
            if (isCrouching)
            {
                jump_disabled = true;
                playerHead.enabled = false;
                if (Input.GetButtonDown("Jump"))
                {
                    //Debug.Log("Slides");
                    Humanoid_Slide();
                }
            }

        }

        if(horizontalDetection > 0 && !isFacingRight)
        {
            Humanoid_FlipDirection();
        } else if(horizontalDetection < 0 && isFacingRight)
        {
            Humanoid_FlipDirection();
        }
    }


    void Humanoid_FlipDirection()
    {
        isFacingRight = !isFacingRight;

        //change appearance

    }

    void Humanoid_Slide()
    {
        player.AddForce(slidingSpeed*slide_Direction);

    }
    


}
