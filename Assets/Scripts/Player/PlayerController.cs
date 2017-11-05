using UnityEngine;
using System.Collections;
using System;

namespace TheWoods.Player
{

    [RequireComponent(typeof(Controller2D))]
    public class PlayerController : MonoBehaviour
    {

        #region DataFields
        [Space(5)]
        [Header("Player Jump Settings")]
        public float jumpHeight = 4;
        public float timeToApex = .4f; //how long should the character take before he reacher the max height
        public float accelerationTimeAirborne;
        public float accelerationTimeGrounded;

        [Space(5)]
        [Header("Player Movement Settings")]
        public float moveSpeed = 6;
        public bool canMove = true;

        //Player physics settings
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
            get { return gravity; }
            set { gravity = value; }
        }
        float jumpVelocity;
        Vector3 velocity;
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        float velocityXSmoothing;

        //Controller Input Settings 
        Vector2 input; //horizontal/vertical inputs.
        
        //Collision Settings.
        Controller2D controllerCollisions2D;


        #endregion DataFields

        public static PlayerController Instance { get { return instance; } } //getter for instance
        static protected PlayerController instance; //declaring instance variable

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("There is alreade a player in play. Deleting old, instantiating new");
                Destroy(Player.PlayerController.Instance.gameObject);
                instance = null;
            }
            else
            {
                instance = this;
            }
        }

        // Use this for initialization
        void Start()
        {
            //init 2D Collision Controller.
            controllerCollisions2D = GetComponent<Controller2D>();

            //physics set up.
            gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
            jumpVelocity = Mathf.Abs(gravity * timeToApex);
        }

        void OnEnable()
        {
            Manager.InputManager.Instance.movementEvent += MobileMovementControls;

            Manager.InputManager.Instance.attackEvent += MobileAttackControls;
            Manager.InputManager.Instance.jumpEvent += MobileJumpControls;
        }

        void OnDisable()
        {
            Manager.InputManager.Instance.movementEvent -= MobileMovementControls;
            Manager.InputManager.Instance.attackEvent -= MobileAttackControls;
            Manager.InputManager.Instance.jumpEvent -= MobileJumpControls;
        }

        #region MobileControls
        //Updates every frame
        /// <summary>
        /// When User presses the mobile joystick button.
        /// </summary>
        public void MobileMovementControls()
        {
            //Add input for the movement in player controller.
            PlayerInputMovement(new Vector2(MobileController.HorizontalInput, MobileController.VerticalInput));
        }

        /// <summary>
        /// Player Attack Behaviour from mobile controls.
        /// </summary>
        public void MobileAttackControls()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Player Jump Behaviour when user presses jump button.
        /// </summary>
        public void MobileJumpControls()
        {
            if (controllerCollisions2D.collisions.below && MobileController.ScreenPressed())
            {
                velocity.y = jumpVelocity;
            }

            PlayerPhysicsMovement();
        }


        #endregion MobileControls

        #region Controller Physics Set-Up

        /// <summary>
        /// Given Horizontal and Vertical input we can move the player in that direction.
        /// Updates when user presses button.
        /// </summary>
        /// <param name="input">Raw Input Data</param>
        public void PlayerInputMovement(Vector2 input)
        {
            controllerCollisions2D.Direction(input);

            //Speed x axis calculation.
            float targetVelocityX = input.x * moveSpeed; //smooth movement on x axis
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controllerCollisions2D.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }

        //Always Updating
        private void PlayerPhysicsMovement()
        {
            //Y-axis movement calculation. //gravity.
            velocity.y += gravity * Time.deltaTime;

            //controller movement.
            controllerCollisions2D.Move(velocity * Time.deltaTime);
        }

        #endregion Controller Physics Set-Up


        void Update()
        {
            if (canMove)
            {
                if (controllerCollisions2D.collisions.above || controllerCollisions2D.collisions.below)
                {
                    velocity.y = 0;
                }

                //PC
                //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                //PC Controls
                controllerCollisions2D.Attack();
                controllerCollisions2D.Invisibility();

                //No input from user required.
                PlayerPhysicsMovement();
            }
        }

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
            if (!controllerCollisions2D.collisions.below) //if there is no ground
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
            if (controllerCollisions2D.collisions.below)
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
            if (Input.GetKeyDown(KeyCode.Space) && controllerCollisions2D.collisions.below)
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
}
