using UnityEngine;
using System.Collections;
using TheWoods.Player;
using TheWoods.Audio;

namespace TheWoods.Enviroment
{
    public class Ladders : MonoBehaviour
    {

        bool playOnce = true;
        void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("Working");
            playClimbSound();

            other.GetComponent<PlayerController>().Gravity = 0;
            other.transform.Translate(new Vector3(0, .1f, 0));
            //ladderControls (other);

        }



        void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Working");
            other.GetComponent<PlayerController>().Gravity = -50;

            playOnce = false;
        }

        //Sound Toggle
        void playClimbSound()
        {
            if (playOnce)
            {
                playOnce = false;
                AudioEventSystem.PlayerLadderClimb();
            }
        }

        //Ladder Controls
        void ladderControls(Collider2D player)
        {

            if (player.GetComponent<PlayerController>().isJumpApex())
            {
                player.GetComponent<PlayerController>().Gravity = 0;
            }//Cancel its momentum

        }
    }
}
