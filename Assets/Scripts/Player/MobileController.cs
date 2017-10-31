using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// HINT: Movement will not work with keyboard while we are in mobile development.
/// TODO: Need to create a screen range in which the user can use the move button. 
/// This way we can separate the touches for different behaviours depending on the screen side. 
/// </summary>


namespace TheWoods.Player
{

    public class MobileController
    {

        //Touch coordinates off the screen.
        private Vector2 touchOrigin;

        private static int horizontalInput;
        private static int verticalInput;

        //Getters
        public static int HorizontalInput { get { return horizontalInput; } }
        public static int VerticalInput { get { return verticalInput; } }

        public MobileController()
        {
            //Init input data.
            horizontalInput = verticalInput = 0;
            touchOrigin = -Vector2.one;
        }

        /// <summary>
        /// Handles the Swipe movement Input.
        /// </summary>
        public void SwipeMovementInput()
        {
            //if there is a touch on the screen.
            if (Input.touchCount > 0)
            {
                //store a reference to the first and only touch currently.
                //Supports only a single finger.
                Touch myTouch = Input.touches[0];

                //if the finger touched the screen.
                if (myTouch.phase == TouchPhase.Began)
                {
                    //Change origin to new position.
                    touchOrigin = myTouch.position;
                }
                else if (myTouch.phase == TouchPhase.Moved && touchOrigin.x >= 0) //if finger is lifted.
                {
                    //Store the touch of when the finger was lifted.
                    Vector2 touchEnd = myTouch.position;

                    //Calculate the distance traveled in x direction.
                    float xDist = touchEnd.x - touchOrigin.x;
                    float yDist = touchEnd.y - touchOrigin.y;

                    //restore touch to off screen default origin.
                    touchOrigin.x = -1;

                    //Check if the swipe is more vertical or more horizontal.
                    if (IsSwipeHorizontal(xDist, yDist))
                    {
                        //User swiped horizontal
                        horizontalInput = xDist > 0 ? 1 : -1;
                    }
                    else
                    {
                        //User swiped vertical
                        verticalInput = yDist > 0 ? 1 : -1;
                    }
                }
            }
        }

        /// <summary>
        /// Checks true if the swipe is more horizontal otherwise false if
        /// swipe is more vertical.
        /// </summary>
        /// <param name="xDist">distance of x-axis swipe</param>
        /// <param name="yDist">distance of y-axis swipe</param>
        /// <returns></returns>
        bool IsSwipeHorizontal(float xDist, float yDist)
        {
            if (Mathf.Abs(xDist) > yDist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}