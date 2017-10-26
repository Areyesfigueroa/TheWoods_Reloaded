using UnityEngine;
using System.Collections;

public class Ladders : MonoBehaviour {

    bool playOnce = true;
	void OnTriggerStay2D(Collider2D other) 
	{
		Debug.Log ("Working");
        playClimbSound();

        other.GetComponent<Player> ().Gravity = 0;
		other.transform.Translate (new Vector3(0,.1f, 0));
		//ladderControls (other);

	}



	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log ("Working");
		other.GetComponent<Player> ().Gravity = -50;

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
		//When player collides cancel its gravity
		Vector3 ladderVelocity = new Vector3(0,1,0);

		if (player.GetComponent<Player> ().isJumpApex()) {
			player.GetComponent<Player> ().Gravity = 0;
		}//Cancel its momentum

	}
}
