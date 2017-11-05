using System.Collections;
using System.Collections.Generic;
using TheWoods.Player;
using TheWoods.Manager;
using UnityEngine;

namespace TheWoods.UI
{
    public class MobileControllerUI : MonoBehaviour
    {
        Player.PlayerController playerController;

        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        public void OnJoyStickButton()
        {
            //Activate Movement Trigger.
            Debug.Log("Movement Triggered");
            InputManager.Instance.MovementTrigger();
        }

        public void OnJoyStickButtonRelease()
        {
            Debug.Log("Movement Release");
            MobileController.ZeroOutInputs();
            playerController.PlayerInputMovement(new Vector2(MobileController.HorizontalInput, MobileController.VerticalInput));
        }

        public void OnJumpButton()
        {
            //Activate Jump Trigger.
            InputManager.Instance.JumpTrigger();
        }

        public void OnAttackButton()
        {
            //Activate Attack Trigger.
            InputManager.Instance.AttackTrigger();
        }
    }
}