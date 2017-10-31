using UnityEngine;
using System.Collections;
using System;

namespace TheWoods.AI
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyAiV2 : MonoBehaviour
    {

        #region Data

        [Space(5)]
        [Header("Choose Your Starting Direction: ")]
        public bool startLeft;
        public bool startRight;

        //Timer
        [Space(5)]
        [Header("Choose When To Delay: ")]
        [Tooltip("Triggers right delay")]
        public bool triggerRightPosDelay = false; //Lets me know which check to toggle
        public bool triggerLeftPosDelay = false;
        private const int leftDir = -1; //toggles on and off to make sure you only check for max reach once
        private const int rightDir = 1;


        [Space(5)]
        [Header("Input how long is the delay: ")]
        public float rightPosDelay = 0;
        public float leftPosDelay = 0;

        //Calculate movement left to right
        [Space(5)]
        [Header("Input the time it takes to reach left and right corners")]
        [Tooltip("How fast should the enemy reach its destination")]
        public float timeTakenDuringLerp = 1f; //time it takes to get from one place to another
        [Space(5)]
        [Header("Input the distance the enemy will move")]
        [Tooltip("Distance value the enemy will travel")]
        public float distance = 2;

        private bool isLerpingRight;
        private bool isLerpingLeft;
        public bool canMove = true;
        bool isGrounded = false;

        Vector2 startPos;
        Vector2 endPos;

        private float timeStartedLerping;
        //rotations
        private float rotRight;
        private float rotLeft;

        #endregion
        void Start()
        {
            //This makes sure you don't have them both checked or unchecked
            if (startRight)
            {
                startLeft = false;
            }
            else if (startLeft)
            {
                startRight = false;
            }
            else
            {
                //Debug.Log("Default is right direction");
                startRight = true;
                rotRight = 180f; //right
            }
            //init
            rotRight = 180f; //right
            rotLeft = 0f; //left
        }

        void FixedUpdate()
        {
            if (isGrounded && canMove)
            {
                Movement();
                Debug.DrawLine(startPos, endPos, Color.red);
            }
        }


        #region Game Functions

        #region Timer Delays
        //Delay time for when you reach maxLeft or maxRight
        IEnumerator Delay(float delay)
        {
            //Debug.Log("Coroutine Start: ");

            //Play the Idle Sound, There is no idle animation
            //        AudioEventSystem.EnemyIdle();

            yield return new WaitForSeconds(delay);

            if (triggerLeftPosDelay && triggerRightPosDelay) //check for both triggers
            {
                if (startRight) //if going right turn left
                {
                    SwitchLerping(leftDir);
                }
                else
                {
                    SwitchLerping(rightDir);
                }
            }

            if (triggerRightPosDelay && !triggerLeftPosDelay) //check for right trigger
            {
                SwitchLerping(leftDir);
            }

            if (triggerLeftPosDelay && !triggerRightPosDelay) //check for left trigger
            {
                SwitchLerping(rightDir);
            }

            //Debug.Log("Coroutine End: ");
        }

        #endregion

        void Rotate(float rotatePos)
        {
            this.transform.Rotate(new Vector3(this.transform.rotation.x, rotatePos));

        }

        void Movement()
        {
            if (isLerpingRight) //Going Right
            {
                float timeSinceStarted = Time.time - timeStartedLerping; //This subtraction means it will start from the point where you leave it on when you start lerp = Start lerp when I decide.
                float percentageComplete = (timeSinceStarted / timeTakenDuringLerp); //calculate the ammount it takes to reach your destination over a period of time.

                transform.position = Vector2.Lerp(startPos, endPos, Mathf.Clamp01(percentageComplete));



                if (Mathf.Clamp01(percentageComplete) >= 1 && triggerRightPosDelay) //works
                {
                    isLerpingRight = false;
                    //Debug.Log("Reached Max Position");
                    StartCoroutine(Delay(rightPosDelay));
                }
                else if (Mathf.Clamp01(percentageComplete) >= 1 && !triggerRightPosDelay)
                {
                    isLerpingRight = false;
                    if (startRight) // if you started on the right side 
                    {
                        SwitchLerping(leftDir); //go left

                    }
                    else if (!startRight)
                    {
                        StartLerping();
                    }
                }

            }


            if (isLerpingLeft) //going left
            {
                float timeSinceStarted = Time.time - timeStartedLerping;
                float percentageComplete = (timeSinceStarted / timeTakenDuringLerp); //calculate the ammount it takes to reach your destination over a period of time.

                //Start from end back to start
                transform.position = Vector2.Lerp(startPos, endPos, Mathf.Clamp01(percentageComplete));

                if (Mathf.Clamp01(percentageComplete) >= 1 && triggerLeftPosDelay)
                {
                    //Debug.Log("Reached Min Position");
                    isLerpingLeft = false;
                    StartCoroutine(Delay(leftPosDelay));
                }
                else if (Mathf.Clamp01(percentageComplete) >= 1 && !triggerLeftPosDelay)
                {
                    //Debug.Log("Reached Min Pos");
                    isLerpingLeft = false;
                    if (startLeft) //if you started on the left
                    {
                        SwitchLerping(rightDir); //goes right

                    }
                    else if (!startLeft) //if you started on right
                    {
                        StartLerping();
                    }

                }
            }

        }


        void StartLerping() //start settings based on startRight or startLeft
        {
            if (startRight)
            {
                isLerpingRight = true;
                endPos = transform.position + Vector3.right * distance; //vector 3 is necessary since transform.position is of three axis'
                Rotate(rotRight);
            }
            else
            {
                isLerpingLeft = true;
                endPos = transform.position + Vector3.left * distance; //vector 3 is necessary since transform.position is of three axis
                Rotate(rotLeft);
            }
            timeStartedLerping = Time.time;
            startPos = transform.position; //anywhere
        }

        void SwitchLerping(int direction)
        {
            if (direction == leftDir)
            {
                isLerpingLeft = true;
                Rotate(rotLeft);
            }
            else if (direction == rightDir)
            {
                isLerpingRight = true;
                Rotate(rotRight);
            }

            timeStartedLerping = Time.time;

            Vector2 temp = startPos; //hold the start pos, move left
            startPos = endPos; //overwrites start with end
            endPos = temp; //overwrite end with start using temp
        }

        //check if we are grounded
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.layer == 11) //Ground
            {
                StartLerping();
                isGrounded = true;
            }
        }
        #endregion
    }
}
