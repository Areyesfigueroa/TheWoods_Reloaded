using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    #region Data
    [Space(5)]
    [Header("Player Jump Settings")]
    public float jumpHeight = 4;
    public float timeToApex = .4f; //how long should the character take before he reacher the max height
    float accelerationTimeAirborne;
    float accelerationTimeGrounded;
	[Space(5)]
	[Header("Player Move Settings")]
    public float moveSpeed= 6;
    public bool canMove = true;

    #region Jump physics formula
    /*
    Known: jumpHeight, timeToJumpApex
    Solve: gravity, jumpVelocity

     //Formula given for jump movement over time
    deltaMovement = velocityInitial * time + (acceleration * time^2/2)
    
    //Code revision formula
    jumpHeight = ((gravity * timeToJumpApex^2)/2)
    2 * jumpHeight = gravity * (timeToApex^2)
    gravity/2*jumpHeight = 1/timeToJumpApex^2
        
    //Formula given for final jump velocity
    velocityFinal = velocityInitial + acceleration * time

    //Code revision formula
    jumpVelocity = gravity * timeToApex
     */
    #endregion
    private float gravity = -20;
	public float Gravity
	{
		get{ return gravity;}
		set{gravity = value; }
	}
    float jumpVelocity;
    Vector3 velocity;
	public Vector3 Velocity
	{
		get{return velocity; }
		set{velocity = value; }
	}
    float velocityXSmoothing;

    //testing 
    [HideInInspector]
    public Vector2 input; //For audio event
    Controller2D controller;


  
    #endregion

    #region Engine Functions

    public static Player Instance { get { return instance; } } //getter for instance
    static protected Player instance; //declaring instance variable

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is alreade a player in play. Deleting old, instantiating new");
            Destroy(Player.Instance.gameObject);
            instance = null;
        }
        else
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2D>();
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToApex);
        print("Gravity: " + gravity + "Jump Velocity: " + jumpVelocity);
	}

    void Update()
    {
        if (canMove)
        {
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
            }
            //Debug.Log ("Y velocity: " +velocity.y);
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //Trigger Movement event
            //Debug.Log("Input: " + input);

            //Rotates the player obj based on direction
            controller.Direction(input);
            controller.Attack();
			controller.Invisibility();

            if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            }

            float targetVelocityX = input.x * moveSpeed; //smooth movement on x axis
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
    #endregion

    #region Helper Functions

    public bool isJumpApex() //Not done
    {
        Debug.Log("Velocity.y" + velocity.y + "Combined: " + velocity.y);

        if (velocity.y == (velocity.y + jumpHeight))
        {
            Debug.Log("Max Jump" + jumpHeight + "Velocity y:" + velocity.y);
            return true;
        }
        else {
            return false;
        }
    }

    //Checks Player Behaviour
    public bool isPlayerMoving() //Used for Audio Event firing
    {
        if (Mathf.Abs(input.x) > 0) //check if he is moving
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPlayerFalling()
    {
        if (!controller.collisions.below) //if there is no ground
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPlayerGrounded()
    {
        if (controller.collisions.below)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPlayerJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
