using System.Collections;
using System.Collections.Generic;
using TheWoods.Player;
using System;
using UnityEngine;

/// <summary>
/// This class is in charge of handle the in-game button inputs. For all devices.
/// </summary>
namespace TheWoods.Manager
{
    public class InputManager : MonoBehaviour
    {
        //Map behaviour to event.
        public Action attackEvent;
        public Action jumpEvent;
        public Action movementEvent;

        //Mobile Controller
        MobileController mobileController;

        //Singleton.
        public static InputManager Instance { get { return instance; } } //getter for instance
        static protected InputManager instance; //declaring instance variable

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("There is alreade a player in play. Deleting old, instantiating new");
                Destroy(InputManager.Instance.gameObject);
                instance = null;
            }
            else
            {
                instance = this;
            }
        }

        void Start()
        {
            //init mobile controller.
            mobileController = new MobileController();
        }

        void Update()
        {
            //Write button input to activate event.
            AndroidControllerInput();

        }

        void AndroidControllerInput()
        {
            //Always read the swipe movement input.
            mobileController.SwipeMovementInput();
        }

        void PCControllerInput()
        {
            if(Input.GetKeyDown("Movement"))
            {
                MovementTrigger();
            }

            if (Input.GetKeyDown("Jump"))
            {
                JumpTrigger();
            }

            if (Input.GetKeyDown("Attack"))
            {
                attackEvent();
            }
        }

        //Event Triggers
        public void MovementTrigger()
        {
            if(movementEvent != null)
            {
                movementEvent();
            }
        }

        public void JumpTrigger()
        {
            if(jumpEvent != null)
            {
                jumpEvent();
            }
        }
        
        public void AttackTrigger()
        {
            if(attackEvent != null)
            {
                attackEvent();
            }
        }
    }
}
