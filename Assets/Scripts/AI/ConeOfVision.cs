using UnityEngine;
using System.Collections;

public class ConeOfVision : MonoBehaviour {

    //Step1: calculate the direction between the enemy and the player
    //Step2: get the forward vector of the enemy
    //For Now grab the player GO
    public GameObject playerGO;
    public float degreesConeOfVision;

	// Update is called once per frame
	void Update ()
    {
        //playerDirection();
        CalculateAngle();
	}

    Vector2 playerDirection()
    {
        Vector2 direction = (playerGO.transform.position - this.transform.position).normalized;
        //Debug.Log("Direction: " + direction);
        return direction;
    }

    void CalculateAngle()
    {
        float angle = Vector2.Angle(playerDirection(), -transform.right);
        //Debug.Log("Angle: " + angle);

        if (angle < degreesConeOfVision)
        {
            Debug.Log("Player is within field of vision");
        }
    }
}
