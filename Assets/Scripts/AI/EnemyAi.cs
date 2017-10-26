using UnityEngine;
using System.Collections;

namespace TheWoods.AI
{
    public class EnemyAi : MonoBehaviour
    {

        #region Data
        //Timer
        [Space(5)]
        [Header("Choose When To Delay: ")]
        [Tooltip("Triggers right delay")]
        public bool triggerRightPosDelay = false; //Lets me know which check to toggle
        public bool triggerLeftPosDelay = false;
        private bool checkLeft = false; //toggles on and off to make sure you only check for max reach once
        private bool checkRight = false;


        [Space(5)]
        [Header("Input how long is the delay: ")]
        public float rightPosDelay = 0;
        public float leftPosDelay = 0;

        private float timeSinceEnded = 0;

        //Calculate movement left to right
        [Space(5)]
        [Header("Input the time it takes to reach left and right corners")]
        [Tooltip("How fast should the enemy reach its destination")]
        public float timeTakenDuringLerp = 1f; //time it takes to get from one place to another
        [Space(5)]
        [Header("Input the distance the enemy will move")]
        [Tooltip("Distance value the enemy will travel")]
        public float distance = 2;

        private bool isLerping;
        public bool canMove = true;
        bool isGrounded = false;
        //testing
        bool runOnce = false;

        Vector2 startPos;
        Vector2 endPos;

        //Rigidbody2D rb;

        private float timeStartedLerping;
        #endregion
        void Start()
        {
            //rb = GetComponent<Rigidbody2D>();
            if (triggerLeftPosDelay)
            {
                checkLeft = true;
            }
            if (triggerRightPosDelay)
            {
                checkRight = true;
            }
        }

        void FixedUpdate()
        {
            if (isGrounded && canMove)
            {
                Movement();
                Debug.DrawLine(startPos, new Vector2(endPos.x + distance, endPos.y), Color.red);
            }
        }


        #region Game Functions

        #region Timer Delays
        //Delay time for when you reach maxLeft or maxRight
        IEnumerator Delay(float delay)
        {
            Debug.Log("Coroutine Start: ");
            yield return new WaitForSeconds(delay);
            Debug.Log("Coroutine End: ");
            if (triggerLeftPosDelay)
            {
                timeStartedLerping = Time.time; //initialize from the far left
                StartCoroutine(CheckLeftToggle(1));
            }
            else if (triggerRightPosDelay)
            {

                //timeStartedLerping = (Time.time - timeSinceEnded); //offSet to start from the final right position
                runOnce = true;
                StartCoroutine(CheckRightToggle(1));
            }
            isLerping = true;
        }

        //This toggles checkleft or right to not get stuck in an infinite checkLoop while the player is at the maxRight or maxLeft
        IEnumerator CheckRightToggle(float time)
        {
            Debug.Log("Checking is off");
            yield return new WaitForSeconds(time);
            Debug.Log("Checking is on");
            checkRight = true;
        }

        IEnumerator CheckLeftToggle(float time)
        {
            Debug.Log("Checking is off");
            yield return new WaitForSeconds(time);
            Debug.Log("Checking is on");
            checkLeft = true;

        }
        #endregion

        void Movement()
        {
            if (isLerping)
            {
                float percentageComplete;
                float timeSinceStarted = Time.time - timeStartedLerping; //This subtraction means it will start from the point where you leave it on when you start lerp = Start lerp when I decide.
                float time = Time.time;
                //Debug.Log("timeSinceStarted: " + timeSinceStarted);
                if (triggerRightPosDelay && runOnce)
                {
                    //change time since started
                    timeSinceStarted = timeSinceEnded;

                    //change timeStartedLerping
                    timeStartedLerping = Time.time - timeSinceStarted;
                    runOnce = false;
                }

                percentageComplete = (timeSinceStarted / timeTakenDuringLerp); //calculate the ammount it takes to reach your destination over a period of time.
                                                                               //Debug.Log("percentageComplete: " + percentageComplete);


                transform.position = Vector2.Lerp(startPos, new Vector2(endPos.x + distance, endPos.y), Mathf.PingPong(percentageComplete, 1f)); //0 init, 1 final 


                // Debug.Log(Mathf.PingPong(percentageComplete, 1f));
                if (Mathf.PingPong(percentageComplete, 1f) >= .99f && checkRight) //check right first// this is stopping the delay. Starting from where 1 left off 
                {


                    Debug.Log("Max Right Delay");
                    //stop player movement
                    isLerping = false;

                    //Stop checking 
                    checkRight = false;

                    //tracks the timer of when you reach max right
                    timeSinceEnded = Time.time;
                    Debug.Log("Timer since ended: " + timeSinceEnded);

                    //Start delay
                    StartCoroutine(Delay(rightPosDelay));

                }

                if (Mathf.PingPong(percentageComplete, 1f) <= 0.01f && checkLeft) //this is stopping the delay. Starting from where 0 left off
                {
                    Debug.Log("Max Left Delay");
                    //stop player movement
                    isLerping = false;

                    //StopChecking
                    checkLeft = false;

                    //Start delay
                    StartCoroutine(Delay(leftPosDelay));
                }
            }
        }

        //Lerping init
        void StartLerping()
        {
            isLerping = true;
            timeStartedLerping = Time.time; //.52f

            startPos = transform.position; //anywhere
            endPos = transform.position; //Distance will dynamically determine its ending position
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
