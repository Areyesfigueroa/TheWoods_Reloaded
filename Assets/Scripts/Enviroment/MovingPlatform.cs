using UnityEngine;
using System.Collections;

namespace TheWoods.Enviroment
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MovingPlatform : MonoBehaviour
    {

        [Space(5)]
        [Header("Movement")]
        public float posDistance = 0.2f;
        public float posSpeed = 1;
        public bool canMove = true;

        [Space(5)]
        [Header("Move Horizontal")]
        public bool isHorizontal = false;

        private Vector3 startPos, leftPos, rightPos;
        private Vector3 upPos, downPos;


        //Triggers

        // Use this for initialization
        void Awake()
        {
            startPos = transform.position;

            //left and right start pos
            leftPos = startPos;
            rightPos = startPos;

            //up and down starting pos
            upPos = startPos;
            downPos = startPos;
        }
        void Start()
        {
            leftPos.x = startPos.x + posDistance; //will give coordinates to the let position
            rightPos.x = startPos.x - posDistance; //rightPos coordinates

            //Up and down starting positions
            upPos.y = startPos.y + posDistance;
            downPos.y = startPos.y - posDistance;
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                if (isHorizontal)
                {
                    horizontalMovement();
                }
                else {
                    verticalMovement();
                }
            }
        }

        //up and down
        void verticalMovement()
        {
            transform.position = Vector3.Lerp(upPos, downPos, Mathf.PingPong(Time.time * posSpeed, 1.0f));
        }

        //left and right
        void horizontalMovement()
        {
            //This will move the passenger left and right 
            //make conditions for when he reaches its destination and returns back to his orignal position and so on.
            transform.position = Vector3.Lerp(leftPos, rightPos, Mathf.PingPong(Time.time * posSpeed, 1.0f));
        }
    }
}
