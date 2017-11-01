using System.Collections;
using System.Collections.Generic;
using TheWoods.Player;
using TheWoods.Manager;
using UnityEngine;

namespace TheWoods.UI
{
    public class MobileControllerUI : MonoBehaviour
    {
        public void OnJoyStickButton()
        {
            //Activate Movement Trigger.
            InputManager.Instance.MovementTrigger();
            
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