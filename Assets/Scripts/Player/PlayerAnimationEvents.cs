using UnityEngine;
using System.Collections;
using TheWoods.Audio;

namespace TheWoods.Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {

        //instance

        public static PlayerAnimationEvents Instance { get { return Instance; } }
        public static PlayerAnimationEvents instance;

        // Use this for initialization
        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("There is already an instance of PlayerAnimationEvents, Deleting old and instantiating a new one");
                Destroy(PlayerAnimationEvents.Instance.gameObject);
                instance = null;
            }
            else
            {
                instance = this;
            }
        }

        public void onPlayerAmbience()
        {
            AudioEventSystem.PlayerAmbience();
        }

        //Player Animation events, Waiting on Animations
        public void onPlayerStep()
        {
            AudioEventSystem.PlayerStep();
        }

        public void onPlayerIdle()
        {
            if (!PlayerController.Instance.isPlayerMoving() && PlayerController.Instance.isPlayerGrounded()) //if player is not moving and is grounded
            {
                AudioEventSystem.PlayerIdle();
            }
        }

        public void onPlayerJump()
        {
            //Falling
            if (PlayerController.Instance.isPlayerJumping())
            {
                AudioEventSystem.PlayerJump();
            }
        }

        public void onPlayerFall()
        {
            if (PlayerController.Instance.isPlayerFalling())
            {
                AudioEventSystem.PlayerFall();
            }
        }

        //Sun Rises
        public void onPlayerDeath()
        {
            AudioEventSystem.PlayerDeath();
        }

        public void onPlayerAttack()
        {
            AudioEventSystem.PlayerAttack();
        }

    }
}
